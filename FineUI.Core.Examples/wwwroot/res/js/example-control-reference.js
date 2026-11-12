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

    function initialize(content, options) {
        var viewerDocument = content.ownerDocument;
        var disposed = false;
        var replacements = [];
        var highlight;
        var statusTimer;
        var status = viewerDocument.createElement('div');
        status.className = 'example-control-reference-status';
        status.setAttribute('role', 'status');
        status.hidden = true;
        viewerDocument.body.appendChild(status);

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
            window.clearTimeout(statusTimer);
            status.textContent = message;
            status.hidden = false;
            statusTimer = window.setTimeout(function () {
                status.hidden = true;
            }, 2500);
        }

        function clearHighlight() {
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
            window.clearTimeout(statusTimer);
            status.hidden = true;
            var exampleWindow = options.getExampleWindow();
            if (!exampleWindow) {
                return;
            }
            var exampleDocument = exampleWindow.document;
            var control = exampleWindow.F && exampleWindow.F.ui[controlId];
            // Target=Top 的 iframe 窗口会在框架页创建实际窗体；只读取已有实例，不触发创建或显示。
            if (control && control instanceof exampleWindow.F.Window) {
                control = exampleWindow.F.getGhostWindow(control) || control;
            }
            var target = control && control.el && control.el[0] || exampleDocument.getElementById(controlId);
            // 未激活 Tab 的内容区隐藏，但标题仍可见；定位标题，不切换当前选项卡。
            if (control && control instanceof exampleWindow.F.Tab && control.headerEl
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
            target.scrollIntoView({ block: 'nearest', inline: 'nearest' });
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
            window.clearTimeout(statusTimer);
            content.removeEventListener('click', onReferenceClick);
            replacements.forEach(function (replacement) {
                replacement.button.replaceWith(replacement.original);
            });
            viewerStylesheet.remove();
            status.remove();
        }

        return { clearHighlight: clearHighlight, dispose: dispose };
    }

    window.FineUIExampleControlReferences = { initialize: initialize };
}());
