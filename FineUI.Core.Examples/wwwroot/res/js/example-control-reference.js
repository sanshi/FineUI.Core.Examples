(function () {
    'use strict';

    var stylesheetUrl = new URL('../css/example-control-reference.css', document.currentScript.src).href;

    function addStylesheet(targetDocument, onLoad, onError) {
        var link = targetDocument.createElement('link');
        link.rel = 'stylesheet';
        link.href = stylesheetUrl;
        link.dataset.exampleControlReferenceStyle = '';
        link.addEventListener('load', onLoad, { once: true });
        link.addEventListener('error', onError, { once: true });
        targetDocument.head.appendChild(link);
        return link;
    }

    function scrollTargetIntoView(target) {
        var targetDocument = target.ownerDocument;
        var targetWindow = targetDocument.defaultView;
        var availableWidth = targetDocument.documentElement.clientWidth;
        var availableHeight = targetDocument.documentElement.clientHeight;
        var margin = 10;

        // 字段可能位于带滚动条的面板内，能否完整显示要同时受内层面板和 iframe 视口的尺寸限制。
        for (var parent = target.parentElement; parent; parent = parent.parentElement) {
            var parentStyle = targetWindow.getComputedStyle(parent);
            if (/(auto|scroll|hidden|clip)/.test(parentStyle.overflowX) && parent.clientWidth > 0) {
                availableWidth = Math.min(availableWidth, parent.clientWidth);
            }
            if (/(auto|scroll|hidden|clip)/.test(parentStyle.overflowY) && parent.clientHeight > 0) {
                availableHeight = Math.min(availableHeight, parent.clientHeight);
            }
        }

        var bounds = target.getBoundingClientRect();
        var computedStyle = targetWindow.getComputedStyle(target);
        var marginNames = ['scroll-margin-top', 'scroll-margin-right', 'scroll-margin-bottom', 'scroll-margin-left'];
        var originalMargins = marginNames.map(function (name) {
            return { name: name, value: target.style.getPropertyValue(name), priority: target.style.getPropertyPriority(name) };
        });

        // 外框向外延伸 4px。滚动时至少留 10px，例如标题框原来贴在 y=0，定位后应从 y=10 开始。
        // scroll-margin 只影响滚动对齐，不占布局空间；保留页面原有的更大边距，并在滚动后恢复行内声明。
        marginNames.forEach(function (name) {
            var existingMargin = parseFloat(computedStyle.getPropertyValue(name)) || 0;
            target.style.setProperty(name, Math.max(margin, existingMargin) + 'px', 'important');
        });
        try {
            // 整个表单高于可视区域时优先露出上边缘，不能为了显示底部而把标题滚到视口上方。
            target.scrollIntoView({
                behavior: 'instant',
                block: bounds.height > availableHeight - margin * 2 ? 'start' : 'nearest',
                inline: bounds.width > availableWidth - margin * 2 ? 'start' : 'nearest'
            });
        } finally {
            originalMargins.forEach(function (original) {
                if (original.value) {
                    target.style.setProperty(original.name, original.value, original.priority);
                } else {
                    target.style.removeProperty(original.name);
                }
            });
        }
    }

    function initialize(content, options) {
        var viewerDocument = content.ownerDocument;
        var disposed = false;
        var replacements = [];
        var highlight;
        var status;

        // 先加载按钮样式，再替换原生强调文字，避免网络较慢时出现带边框的默认按钮。
        var viewerStylesheet = addStylesheet(viewerDocument, enableReferences, function () {
            // 样式不可用时保留原生强调文字，描述仍可正常阅读。
        });

        function enableReferences() {
            if (disposed) {
                return;
            }
            content.querySelectorAll('[data-example-control]').forEach(function (original) {
                var button = viewerDocument.createElement('button');
                var label = original.title || original.textContent;
                button.type = 'button';
                button.className = original.className;
                button.dataset.exampleControl = original.dataset.exampleControl;
                button.textContent = original.textContent;
                button.title = '在示例中定位：' + label;
                button.setAttribute('aria-label', button.title);
                original.replaceWith(button);
                replacements.push({ original: original, button: button });
            });
            content.addEventListener('click', onReferenceClick);
        }

        function showStatus(message) {
            clearStatus();
            var exampleWindow = options.getExampleWindow();
            if (!exampleWindow || disposed) {
                return;
            }

            // 提示显示在被定位的示例中央，即使描述区位于底部，也能明确对应当前示例。
            // 等待样式加载后再显示，避免先在页面底部出现一段无样式文字。
            var exampleDocument = exampleWindow.document;
            var element = exampleDocument.createElement('div');
            element.className = 'example-control-reference-status';
            element.setAttribute('role', 'status');
            element.textContent = message;
            element.hidden = true;
            exampleDocument.body.appendChild(element);
            var current = { element: element, stylesheet: null, timer: 0 };
            status = current;
            current.stylesheet = addStylesheet(exampleDocument, function () {
                if (status !== current) {
                    return;
                }
                element.hidden = false;
                current.timer = window.setTimeout(clearStatus, 2500);
            }, function () {
                if (status === current) {
                    clearStatus();
                }
            });
        }

        function clearStatus() {
            if (!status) {
                return;
            }
            window.clearTimeout(status.timer);
            status.element.remove();
            status.stylesheet.remove();
            status = null;
        }

        function clearHighlight() {
            // 详情区关闭或切换示例时，示例中的提示也要清除，不能留在已无关联的页面上。
            clearStatus();
            if (!highlight) {
                return;
            }
            window.cancelAnimationFrame(highlight.frame);
            window.clearTimeout(highlight.timer);
            highlight.overlay.remove();
            highlight.stylesheet.remove();
            highlight = null;
        }

        function onReferenceClick(event) {
            var reference = event.target.closest('button[data-example-control]');
            if (!reference || !content.contains(reference)) {
                return;
            }
            highlightControl(reference.dataset.exampleControl);
        }

        function highlightControl(controlId) {
            clearHighlight();
            var exampleWindow = options.getExampleWindow();
            if (!exampleWindow) {
                return;
            }
            var exampleDocument = exampleWindow.document;
            // 释放全局 F 的页面仍保留 FineUI；引用定位与示例本身使用同一套控件实例。
            var framework = exampleWindow.FineUI || exampleWindow.F;
            var control = framework && framework.ui && framework.ui[controlId];
            // Target=Top 的 iframe 窗口会在框架页创建实际窗体；只读取已有实例，不触发创建或显示。
            if (control && control instanceof framework.Window) {
                control = framework.getGhostWindow(control) || control;
            }
            var target = control && control.el && control.el[0] || exampleDocument.getElementById(controlId);
            // 未激活 Tab 的内容区隐藏，但标题仍可见；定位标题，不切换当前选项卡。
            if (control && control instanceof framework.Tab && control.headerEl
                && isVisibleTarget(control.headerEl[0])) {
                target = control.headerEl[0];
            }
            if (!target) {
                showStatus('当前示例中未找到该控件。');
                return;
            }
            if (!isVisibleTarget(target)) {
                showStatus('该控件当前未显示，请先在示例中显示它。');
                return;
            }

            // 只滚动到控件所在位置，不点击、聚焦或改值；独立外框不占布局空间。
            scrollTargetIntoView(target);
            var targetDocument = target.ownerDocument;
            var overlay = targetDocument.createElement('div');
            overlay.className = 'example-control-reference-highlight';
            overlay.dataset.controlId = controlId;
            overlay.setAttribute('aria-hidden', 'true');
            overlay.hidden = true;
            targetDocument.body.appendChild(overlay);
            var current = { overlay: overlay, stylesheet: null, frame: 0, timer: 0 };
            highlight = current;
            current.stylesheet = addStylesheet(targetDocument, function () {
                if (highlight !== current) {
                    return;
                }
                overlay.hidden = false;
                current.timer = window.setTimeout(clearHighlight, 2000);
                trackTarget();
            }, function () {
                if (highlight === current) {
                    clearHighlight();
                    showStatus('定位样式加载失败，请稍后重试。');
                }
            });

            function trackTarget() {
                var connectedWindow = options.getExampleWindow();
                if (highlight !== current) {
                    return;
                }
                if (!target.isConnected || !isVisibleTarget(target)
                    || connectedWindow !== exampleWindow || connectedWindow.document !== exampleDocument) {
                    clearHighlight();
                    return;
                }
                var bounds = target.getBoundingClientRect();
                overlay.style.left = bounds.left + 'px';
                overlay.style.top = bounds.top + 'px';
                overlay.style.width = bounds.width + 'px';
                overlay.style.height = bounds.height + 'px';
                // 计时与跟随放在框架页，关闭或切换描述 Tab 后也能及时清理外框。
                current.frame = window.requestAnimationFrame(trackTarget);
            }
        }

        function isVisibleTarget(target) {
            // 隐藏标签可能被移到屏幕外，仍有矩形；不能把它当作当前可见内容进行定位。
            // 无标题栏的标签则定位其可见内容，不为定位自动切换标签。
            return target && target.getClientRects().length > 0
                && !target.closest('.f-hidden-offsets')
                && target.ownerDocument.defaultView.getComputedStyle(target).visibility !== 'hidden';
        }

        function dispose() {
            disposed = true;
            clearHighlight();
            content.removeEventListener('click', onReferenceClick);
            replacements.forEach(function (replacement) {
                replacement.button.replaceWith(replacement.original);
            });
            viewerStylesheet.remove();
        }

        return { clearHighlight: clearHighlight, dispose: dispose };
    }

    window.FineUIExampleControlReferences = { initialize: initialize };
}());
