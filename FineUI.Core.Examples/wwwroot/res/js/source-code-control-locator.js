/**
 * 源码查看器中的控件定位：把页面描述里的控件引用变成按钮，点击后滚动到示例控件并短暂显示高亮外框。
 * 由 source-code-viewer.js 在 Region 模式且描述含 data-example-control 时按需加载；独立 Window 模式不加载定位模块。
 * 查看器调用 FineUISourceCodeControlLocator.initialize(content, { getExampleWindow })，content 是描述容器，getExampleWindow 返回仍与该描述关联的同源示例窗口。
 * 返回控制器提供 clearHighlight() 和 dispose()：前者清除高亮及提示，后者还恢复引用文字并移除监听。
 * 模块按需加载同名 CSS，不模拟点击、聚焦或赋值；隐藏控件只提示原因，控件较大时优先露出上边缘。
 */

(function () {
    'use strict';

    var stylesheetUrl = new URL('../css/source-code-control-locator.css', document.currentScript.src).href;

    /**
     * 把定位样式加载到指定文档，使描述按钮或示例里的外框使用同一套样式。
     * 内部调用 addStylesheet(targetDocument, onLoad, onError)；返回 link 节点，由对应控制器在清理时移除。
     */
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

    /**
     * 滚动外层和内层滚动区域，让目标尽量完整显示并留出至少 10px；目标过大时优先显示上边缘。
     * 由高亮流程调用 scrollTargetIntoView(target)，target 为实际 DOM 元素；临时滚动边距在处理结束后恢复，不改变布局。
     */
    function scrollTargetIntoView(target) {
        var targetDocument = target.ownerDocument;
        var targetWindow = targetDocument.defaultView;
        var availableWidth = targetDocument.documentElement.clientWidth;
        var availableHeight = targetDocument.documentElement.clientHeight;
        var margin = 10;

        // 页面根节点按 iframe 的真实视口计算。绝对定位的面板可能铺满 548px，而 body 内容盒只有 48px，不能据此判定面板放不下。
        // 字段位于内层滚动面板时，再按该面板的可视范围限制尺寸。
        for (var parent = target.parentElement; parent; parent = parent.parentElement) {
            if (parent === targetDocument.body || parent === targetDocument.documentElement) {
                break;
            }
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

        // 有滚动空间时至少留 10px，例如标题框原来贴在 y=0，定位后应从 y=10 开始，便于辨认整个控件。
        // scroll-margin 只影响滚动对齐，不占布局空间；保留页面原有的更大边距，并在滚动后恢复行内声明。
        marginNames.forEach(function (name) {
            var existingMargin = parseFloat(computedStyle.getPropertyValue(name)) || 0;
            target.style.setProperty(name, Math.max(margin, existingMargin) + 'px', 'important');
        });
        try {
            // 548px 的面板恰好放进 548px 的 iframe 时，不能扣掉留白后将它误判为超大控件。
            // 只有控件本身超出可视区域才强制对齐上边缘；其余就近滚动，避免带动已完整显示的外层框架。
            target.scrollIntoView({
                behavior: 'instant',
                block: bounds.height > availableHeight ? 'start' : 'nearest',
                inline: bounds.width > availableWidth ? 'start' : 'nearest'
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

    /**
     * 让一段页面描述具备点击控件引用后定位的能力，并维护本次描述专属的高亮与提示。
     * 由源码查看器调用 initialize(content, options)；options.getExampleWindow() 应只返回当前仍可定位的示例窗口，否则返回 null。
     * 返回 clearHighlight 和 dispose；描述切换或销毁时调用 dispose，关闭详情区时可只调用 clearHighlight。
     */
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

        /**
         * 样式加载完成后，把描述中的 data-example-control 强调文字替换为可键盘操作的定位按钮。
         * 仅由 initialize 的样式加载回调调用；初始化已释放时不再替换，原文字保存在 replacements 中以便恢复。
         */
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

        /**
         * 在当前示例可视区域中央短暂提示无法定位的原因，避免消息出现在远离控件的描述区。
         * 内部调用 showStatus(message)，样式就绪后显示约 2.5 秒；重复定位会先清除上一次提示。
         */
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

        /**
         * 移除本次定位提示、延迟计时器及为提示加载的样式。
         * 由新提示、高亮清理或释放流程调用；没有提示时可重复调用。
         */
        function clearStatus() {
            if (!status) {
                return;
            }
            window.clearTimeout(status.timer);
            status.element.remove();
            status.stylesheet.remove();
            status = null;
        }

        /**
         * 清除当前高亮、跟随位置的动画帧以及未消失的定位提示。
         * 由控制器对外提供 clearHighlight()；关闭详情或重新定位时调用，不恢复描述中的定位按钮。
         */
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

        /**
         * 从描述容器的点击事件中找出定位按钮，再按其 data-example-control 解析目标。
         * 作为描述容器的事件委托回调登记；仅处理当前 content 内部的按钮。
         */
        function onReferenceClick(event) {
            var reference = event.target.closest('button[data-example-control]');
            if (!reference || !content.contains(reference)) {
                return;
            }
            highlightControl(reference.dataset.exampleControl);
        }

        /**
         * 查找当前示例里的实际控件，滚动到可见位置并创建约两秒的独立高亮外框。
         * 内部调用 highlightControl(controlId)，controlId 必须是实际客户端 ID；隐藏或未创建时只提示，不自动显示或创建控件。
         */
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

            /**
             * 在高亮期间跟随目标的位置和尺寸，避免滚动或布局变化后外框停在旧位置。
             * 由样式加载成功后启动，之后通过 requestAnimationFrame 自己续调；目标移除、隐藏或示例切换时结束并清理。
             */
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

        /**
         * 判断目标当前是否可被定位，排除无矩形、偏移隐藏及 visibility 隐藏的元素。
         * 内部调用 isVisibleTarget(target)；未激活标签内容不能因为仍有尺寸就当成可见。
         */
        function isVisibleTarget(target) {
            // 隐藏标签可能被移到屏幕外，仍有矩形；不能把它当作当前可见内容进行定位。
            // 无标题栏的标签则定位其可见内容，不为定位自动切换标签。
            return target && target.getClientRects().length > 0
                && !target.closest('.f-hidden-offsets')
                && target.ownerDocument.defaultView.getComputedStyle(target).visibility !== 'hidden';
        }

        /**
         * 释放这一段描述的全部定位资源，并把定位按钮恢复成原来的强调文字。
         * 由源码查看器在更换描述或示例时调用返回控制器的 dispose()；调用后不再启用本次引用。
         */
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

    window.FineUISourceCodeControlLocator = { initialize: initialize };
}());
