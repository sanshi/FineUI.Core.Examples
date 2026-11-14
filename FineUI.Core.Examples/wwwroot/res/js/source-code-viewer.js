/**
 * 页面描述和源代码查看器的共同入口，负责打开、关闭、跟随活动示例，以及连接示例与源码页。
 * 由示例公共布局、框架入口、源码页及原生示例辅助页加载；用 FineUI 或 F 获取运行时，兼容释放全局 F 的示例。
 * 示例或框架用 meta[name="fineui-example-source"] 指定源码查看页；框架另通过 PARAMS 指定工作区和详情 Region，源码页使用 fineui-source-code-viewer 元信息区分自身角色。
 * 框架工具调用 FineUISourceCodeViewer.toggle()，宽度足够时显示底部或右侧 Region；独立示例由自动创建的右下角按钮打开 F.Window。
 * 源码页通过父页面 FineUISourceCodeViewer.connect(window) 取得描述与关闭操作，Region 中有引用时再加载 source-code-control-locator.js。
 * 脚本自动登记初始化和 Escape 处理，并用 localStorage 记录 Region 的展开状态、位置及用户拖动尺寸。
 */

(function () {
    'use strict';
    // noConflict 示例会把全局 F 恢复为原来的值；查看器持有框架本身，仍能创建窗口和操作选项卡。
    var F = window.FineUI || window.F;
    var viewerScriptUrl = document.currentScript.src;

    /**
     * 在文档和 FineUI 都准备完成后初始化查看器，兼容异步加载脚本及已经就绪的页面。
     * 内部调用 whenPageReady(callback)；HTML 仍在解析时先等待 DOMContentLoaded，避免错过后续内联脚本登记的控件。
     */
    function whenPageReady(callback) {
        // HTML 尚未接收完整时，后面的内联脚本可能还没有登记创建控件的回调，先等页面解析完。
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', function () {
                whenPageReady(callback);
            }, { once: true });
        } else if (F.pageLoaded) {
            callback();
        } else {
            F.ready(callback);
        }
    }

    var viewerMode = document.querySelector('meta[name="fineui-source-code-viewer"]');
    if (viewerMode) {
        if (viewerMode.content === 'source') {
            whenPageReady(initializeSourcePage);
        } else if (viewerMode.content === 'file') {
            document.addEventListener('keydown', onSourceFileKeyDown);
        }
        return;
    }


    /**
     * 让源码页连接打开它的查看器，插入页面描述，并在 Region 中增加换位与关闭工具。
     * 由 source 角色页面的就绪流程调用；独立打开源码页时没有父连接，仍保留原有源文件显示。
     */
    function initializeSourcePage() {
        var tabsAddress = document.querySelector('meta[name="fineui-source-tabs"]');
        var tabs = F(tabsAddress ? tabsAddress.content : 'TabStrip1');
        var connection;
        try {
            if (parent !== window && parent.FineUISourceCodeViewer) {
                connection = parent.FineUISourceCodeViewer.connect(window);
            }
        } catch (error) {
            // 源码页可以独立打开；父页面不提供查看器时仍显示原有源文件。
        }
        if (!tabs || !connection) {
            return;
        }

        window.FineUISourceCodeViewer = { close: connection.close };
        var content = document.getElementById('exampleDescriptionContent');
        if (content && connection.description) {
            content.replaceChildren(document.importNode(connection.description, true));
            if (connection.regionMode) {
                connection.initializeControlReferences(content);
            }
        }
        document.title = '页面描述和源代码 · ' + connection.title;
        document.addEventListener('keydown', onSourcePageKeyDown);

        /**
         * 在聚合源码页按 Escape 时关闭与之关联的查看器。
         * 源码页连接建立后登记到 document.keydown，通过本次 connection.close() 关闭。
         */
        function onSourcePageKeyDown(event) {
            if (event.key === 'Escape') {
                connection.close();
            }
        }

        if (connection.regionMode) {
            // 框架中的两个选项卡区域复用首页样式，使标题高度、边线和圆角保持一致。
            document.body.classList.add('bodyregion');
            tabs.addTool({
                type: 'Tool',
                id: 'toolPositionDetails',
                iconFont: 'f-iconfont-right',
                handler: onPositionClick
            });
            tabs.addTool({
                type: 'Tool',
                id: 'toolCloseDetails',
                iconFont: 'f-iconfont-close',
                tooltip: '关闭',
                handler: connection.close
            });
            F('toolCloseDetails').el.attr('aria-label', '关闭');
            updatePositionTool();
            tabs.doLayout();
        }

        /**
         * 根据详情区当前在下方或右侧，更新换位工具的方向图标、提示及可访问名称。
         * 由源码页初始化或换位后调用；方向通过当前连接的 getPosition() 读取。
         */
        function updatePositionTool() {
            var isBottom = connection.getPosition() === 'bottom';
            var label = isBottom ? '移到右侧' : '移到下方';
            var tool = F('toolPositionDetails');
            tool.setIconFont(isBottom ? 'f-iconfont-right' : 'f-iconfont-down');
            tool.setTooltip(label);
            tool.el.attr('aria-label', label);
        }

        /**
         * 把框架详情区从下方移到右侧，或从右侧移回下方，再更新工具提示。
         * 绑定到 Region 源码选项卡的换位工具 click，通过当前连接调用 switchPosition()。
         */
        function onPositionClick(event) {
            connection.switchPosition();
            updatePositionTool();
        }
    }

    /**
     * 让单个源文件 iframe 中的 Escape 也能关闭父级查看器。
     * 仅在 file 角色页面登记到 keydown；父页须提供 FineUISourceCodeViewer.close。
     */
    function onSourceFileKeyDown(event) {
        if (event.key === 'Escape' && parent !== window && parent.FineUISourceCodeViewer) {
            parent.FineUISourceCodeViewer.close();
        }
    }

    var sourceAddress = document.querySelector('meta[name="fineui-example-source"]');
    if (!sourceAddress) {
        return;
    }
    var nativeSourceHost = document.querySelector('meta[name="fineui-native-source-host"]');

    var detailsHost;
    var windowHost;
    var launcher;
    var regionMode = false;
    var currentExample;
    var currentAddress;
    var watchedFrame;
    var watchedNestedFrame;
    var watchedContainerWindow;
    var referenceController;
    var referenceModulePromise;
    var preferredWidth = 480;
    var preferredHeight = 360;
    var regionPosition = 'bottom';
    var returnFocus;
    var sizeUpdateFrame;
    var pendingOpen = false;
    var preferenceKey = 'FineUI.SourceCodeViewer.' + sourceAddress.content;

    /**
     * 判断实际示例是否提供了非空页面描述，避免创建空白描述标签。
     * 内部调用 needsDescriptionTab(exampleWindow)，读取示例的 example-description 模板。
     */
    function needsDescriptionTab(exampleWindow) {
        var description = exampleWindow.document.getElementById('example-description');
        return description && description.content.textContent.trim().length > 0;
    }

    /**
     * 取得当前框架中承载主演示和详情区的工作区控件。
     * 内部调用 getWorkspace()；通过 PARAMS.exampleWorkspace 查找，独立示例没有工作区时返回 null。
     */
    function getWorkspace() {
        if (window.PARAMS && PARAMS.exampleWorkspace) {
            return F(PARAMS.exampleWorkspace);
        }
        return null;
    }

    /**
     * 校验保存的尺寸，防止损坏或过大的存储值破坏详情布局。
     * 恢复偏好时传入原值；仅接受 1 到 8192 的有限数值并取整，不合法时返回 undefined。
     */
    function readPreferredSize(value) {
        return typeof value === 'number' && Number.isFinite(value) && value >= 1 && value <= 8192
            ? Math.round(value)
            : undefined;
    }

    /**
     * 恢复详情是否展开、所在方向和用户拖动尺寸，同时兼容旧存储键。
     * 在框架工作区初始化后调用；存储不可用或内容损坏时继续采用当前默认值。
     */
    function restorePreferences() {
        try {
            var preferences = JSON.parse(window.localStorage.getItem(preferenceKey)
                || window.localStorage.getItem('FineUI.Core.Examples.Details.' + sourceAddress.content));
            if (!preferences || typeof preferences !== 'object' || Array.isArray(preferences)) {
                return;
            }
            if (preferences.position === 'bottom' || preferences.position === 'right') {
                regionPosition = preferences.position;
            }
            preferredWidth = readPreferredSize(preferences.width) || preferredWidth;
            preferredHeight = readPreferredSize(preferences.height) || preferredHeight;
            pendingOpen = preferences.visible === true;
        } catch (error) {
            // 浏览器禁止存储或旧数据损坏时，仍使用默认布局打开示例。
        }
    }

    /**
     * 保存框架详情布局，让刷新后继续使用相同位置与尺寸。
     * 打开、关闭、换位或拖动后调用；只保存偏好尺寸，不保存表单内容，也不把窗口缩小时的临时限制当成用户选择。
     */
    function savePreferences() {
        if (!getWorkspace()) {
            return;
        }
        try {
            // 只保存详情布局，不保存表单数据；缩小浏览器后的临时尺寸不覆盖用户拖动偏好。
            window.localStorage.setItem(preferenceKey, JSON.stringify({
                visible: pendingOpen || !!(detailsHost && !detailsHost.hidden),
                position: regionPosition,
                width: preferredWidth,
                height: preferredHeight
            }));
        } catch (error) {
            // 存储被禁用或空间不足时，当前页面的显示、切换和拖动仍可使用。
        }
    }

    /**
     * 把焦点交回打开查看器前仍然存在的元素，便于继续操作示例。
     * 由关闭流程调用；原元素已从页面移除时不尝试聚焦。
     */
    function restoreFocus() {
        if (returnFocus && returnFocus.isConnected) {
            returnFocus.focus();
        }
    }

    /**
     * 关闭当前详情承载控件，清除示例高亮，并保存关闭状态和恢复焦点。
     * 由连接对象的 close、框架工具及 Escape 流程调用；只隐藏承载控件，保留已加载的源码 iframe 供下次使用。
     */
    function closeDetails() {
        if (referenceController) {
            referenceController.clearHighlight();
        }
        pendingOpen = false;
        if (detailsHost) {
            detailsHost.hide();
        }
        savePreferences();
        restoreFocus();
    }

    /**
     * 在当前详情区的显示和隐藏之间切换。
     * 作为 FineUISourceCodeViewer.toggle() 对外提供，供框架的源码工具按钮调用。
     */
    function toggleDetails() {
        if (pendingOpen || (detailsHost && !detailsHost.hidden)) {
            closeDetails();
        } else {
            openActiveExample();
        }
    }

    /**
     * 为独立示例或较窄的框架创建可调整大小的模态源码窗口。
     * 由 ensureHost 首次需要 Window 时调用；窗口初始隐藏，真正打开时再设置并加载源码地址。
     */
    function createWindow() {
        windowHost = F.create({
            type: 'Window',
            id: 'exampleDetailsWindow',
            title: '页面描述和源代码',
            hidden: true,
            modal: true,
            resizable: true,
            maximizable: true,
            maximized: window.innerWidth <= 700,
            minWidth: Math.min(360, window.innerWidth),
            minHeight: Math.min(200, window.innerHeight),
            width: Math.min(1000, window.innerWidth - 32),
            height: Math.min(720, window.innerHeight - 32),
            constrainSize: true,
            keepLastSize: true,
            iframe: true,
            iframeDelay: true,
            clearIFrameAfterClose: false,
            reloadIFrameOnShow: false,
            listeners: {
                hide: function (event) {
                    pendingOpen = false;
                    if (launcher) {
                        launcher.hidden = false;
                    }
                    savePreferences();
                    restoreFocus();
                    if (nativeSourceHost && parent.FineUISourceViewerNative) {
                        parent.FineUISourceViewerNative.close();
                    }
                }
            }
        });
    }

    /**
     * 按工作区可用尺寸限制详情区宽高，同时为主演示保留空间。
     * Region 打开、换位或工作区 resize 后调用；仅限制实际显示尺寸，不覆盖用户偏好尺寸。
     */
    function updateRegionSize() {
        if (!regionMode || !detailsHost || detailsHost.hidden) {
            return;
        }
        var workspace = getWorkspace();
        var splitWidth = detailsHost.splitWidth;
        if (regionPosition === 'right') {
            // 内容区为 1000px 时，右侧详情最多占 519px，给主演示保留 480px 和 1px 分隔条。
            var availableWidth = workspace.bodyEl.width();
            var mainWidth = Math.min(480, Math.floor(availableWidth * 0.55));
            var maximumWidth = Math.max(1, availableWidth - mainWidth - splitWidth);
            detailsHost.maxHeight = undefined;
            detailsHost.setMinHeight(0);
            detailsHost.maxWidth = maximumWidth;
            detailsHost.setMinWidth(Math.min(320, maximumWidth));
            detailsHost.setWidth(Math.min(preferredWidth || 480, maximumWidth));
        } else {
            // 下方详情保留整行宽度，并为上方主演示预留至少一部分高度，避免拖动后看不到示例。
            var availableHeight = workspace.bodyEl.height();
            var mainHeight = Math.min(320, Math.floor(availableHeight * 0.55));
            var maximumHeight = Math.max(1, availableHeight - mainHeight - splitWidth);
            detailsHost.maxWidth = undefined;
            detailsHost.setMinWidth(0);
            detailsHost.maxHeight = maximumHeight;
            detailsHost.setMinHeight(Math.min(220, maximumHeight));
            detailsHost.setHeight(Math.min(preferredHeight || 360, maximumHeight));
        }
    }

    /**
     * 把同一轮连续尺寸变化合并为一次详情布局更新。
     * 绑定到工作区 resize；通过 requestAnimationFrame 调用 updateRegionSize，避免重复重排。
     */
    function onWorkspaceResize(event) {
        if (sizeUpdateFrame) {
            window.cancelAnimationFrame(sizeUpdateFrame);
        }
        sizeUpdateFrame = window.requestAnimationFrame(function () {
            sizeUpdateFrame = undefined;
            updateRegionSize();
        });
    }

    /**
     * 窗口变窄时降低源码窗口的最小宽高，保持关闭等操作仍在可视区域。
     * 登记到浏览器 resize；窗口已创建时更新约束，已显示时再重新布局。
     */
    function onViewportResize(event) {
        if (!windowHost) {
            return;
        }
        // 桌面打开后缩到 320px 时，不能继续保留 360px 最小宽度，否则关闭按钮会被挤出屏幕。
        windowHost.setMinWidth(Math.min(360, window.innerWidth));
        windowHost.setMinHeight(Math.min(200, window.innerHeight));
        if (!windowHost.hidden) {
            windowHost.doLayout();
        }
    }

    /**
     * 为本次打开选择 Region 或 Window，并在承载方式变化时要求重新加载对应源码页。
     * 由 openDetails 调用；工作区宽度至少 800px 才使用 Region，已显示时不因浏览器缩放搬动正在阅读的内容。
     */
    function ensureHost() {
        if (detailsHost && !detailsHost.hidden) {
            return;
        }
        var workspace = getWorkspace();
        // 内容不足 800px 时，两个选项卡区域难以同时阅读，改用同一套普通窗口。
        // 只在打开时选择承载位置，浏览器缩放不会搬动或重新加载正在阅读的源码。
        var useRegion = workspace && workspace.bodyEl.width() >= 800;
        var nextHost;
        if (useRegion) {
            nextHost = F(PARAMS.exampleDetails);
            // 服务端只有 Window 提供关闭清空属性；普通 Panel 在浏览器显式保留 iframe，关闭后可继续阅读。
            nextHost.clearIFrameAfterClose = false;
            nextHost.setRegionPosition(regionPosition);
        } else {
            if (!windowHost) {
                createWindow();
            }
            nextHost = windowHost;
        }
        if (detailsHost !== nextHost) {
            currentAddress = null;
        }
        detailsHost = nextHost;
        regionMode = !!useRegion;
    }

    /**
     * 把用户拖动后的 Region 宽度或高度记录为下一次打开的偏好。
     * 绑定到详情 Region 的 splitdrag，也通过 FineUISourceCodeViewer.rememberSize() 提供；只记录当前方向的尺寸。
     */
    function rememberSize() {
        if (regionMode && detailsHost) {
            if (regionPosition === 'right') {
                preferredWidth = Math.min(detailsHost.width, detailsHost.maxWidth);
            } else {
                preferredHeight = Math.min(detailsHost.height, detailsHost.maxHeight);
            }
            savePreferences();
        }
    }

    /**
     * 在 Region 的 bottom 与 right 之间切换，更新尺寸限制并保存偏好。
     * 由源码页连接调用 switchPosition()，返回当前方向；Window 模式不改变承载位置。
     */
    function switchPosition() {
        if (regionMode && detailsHost) {
            regionPosition = regionPosition === 'bottom' ? 'right' : 'bottom';
            detailsHost.setRegionPosition(regionPosition);
            updateRegionSize();
            savePreferences();
        }
        return regionPosition;
    }

    /**
     * 让源码页读取父框架详情区方向，以便设置换位工具。
     * 通过 connect 返回的连接对象调用 getPosition()，返回 bottom 或 right。
     */
    function getPosition() {
        return regionPosition;
    }

    /**
     * 示例 iframe 加载或重新导航后，更新仍处于打开状态的详情内容。
     * 由 watchExample 和手机预览跟随过程登记到 load；不会因为普通示例加载就自动打开已关闭的详情。
     */
    function onExampleFrameLoad(event) {
        if ((pendingOpen || (detailsHost && !detailsHost.hidden)) && watchedContainerWindow) {
            openDetails(watchedContainerWindow);
        }
    }

    /**
     * 跟踪当前示例容器的 iframe 加载，换标签时移除旧 iframe 的监听。
     * 由 openDetails(exampleWindow) 内部调用，传入主选项卡中的容器窗口。
     */
    function watchExample(exampleWindow) {
        watchedContainerWindow = exampleWindow;
        var frame = exampleWindow.frameElement;
        if (frame === watchedFrame) {
            return;
        }
        if (watchedFrame) {
            watchedFrame.removeEventListener('load', onExampleFrameLoad);
        }
        watchedFrame = frame;
        if (watchedFrame) {
            watchedFrame.addEventListener('load', onExampleFrameLoad);
        }
    }

    /**
     * 手机预览已有内部 iframe 时取得真正的示例窗口，并监听其后续导航。
     * 内部传入容器窗口；fineui-example-frame 元信息提供 iframe 选择器，尚未创建实际 iframe 时返回空值。
     */
    function resolveExampleWindow(containerWindow) {
        var preview = containerWindow.document.querySelector('meta[name="fineui-example-frame"]');
        var frame = preview && containerWindow.document.querySelector(preview.content);
        if (frame !== watchedNestedFrame) {
            if (watchedNestedFrame) {
                watchedNestedFrame.removeEventListener('load', onExampleFrameLoad);
            }
            watchedNestedFrame = frame;
            if (watchedNestedFrame) {
                watchedNestedFrame.addEventListener('load', onExampleFrameLoad);
            }
        }
        // 手机预览已有 iframe，只跟随其中的实际示例；尚未加载时等待它的 load，不显示预览容器源码。
        return preview ? frame && frame.contentWindow : containerWindow;
    }

    /**
     * 为指定同源示例显示描述和源码，收集附加源文件，并复用同一示例已经加载的查看器。
     * 内部调用 openDetails(exampleWindow)；先解析手机预览的实际子页，跨域、空白页或源码辅助页不作为普通示例处理。
     */
    function openDetails(exampleWindow) {
        if (referenceController) {
            referenceController.clearHighlight();
        }
        var address;
        try {
            // 新页签刚激活时可能仍是 about:blank，先监听加载，实际示例就绪后再更新详情。
            watchExample(exampleWindow);
            exampleWindow = resolveExampleWindow(exampleWindow);
            if (!exampleWindow) {
                return;
            }
            address = new URL(exampleWindow.location.href);
            if (address.origin !== window.location.origin) {
                return;
            }
            if (exampleWindow.document.querySelector('meta[name="fineui-source-code-viewer"]')) {
                return;
            }
        } catch (error) {
            return;
        }

        returnFocus = document.activeElement;
        var exampleWindowChanged = currentExample !== exampleWindow;
        currentExample = exampleWindow;
        pendingOpen = false;
        ensureHost();

        var files = [address.pathname + address.search];
        var additionalFiles = exampleWindow.document.querySelector('meta[name="sourcefiles"]');
        if (additionalFiles) {
            files = files.concat(additionalFiles.content.split(';'));
        }
        var viewerAddress = sourceAddress.content + '?files=' + encodeURIComponent(files.join(';'))
            + '&description=' + (needsDescriptionTab(exampleWindow) ? '1' : '0');

        // 同一窗口、同一地址继续复用查看器，不移动主演示 iframe。
        // 外层手机预览刷新后，即使子页地址仍是 Mobile/Button/Button，内部 iframe 也已重建；
        // 此时重新加载查看器，让描述引用连接到新子窗口，不能继续使用已关闭的旧窗口。
        if (currentAddress !== viewerAddress || exampleWindowChanged) {
            if (referenceController) {
                referenceController.dispose();
                referenceController = null;
            }
            currentAddress = viewerAddress;
            detailsHost.setIFrameUrl(viewerAddress);
        }
        if (launcher && detailsHost === windowHost) {
            // 示例窗口的遮罩不能挡住入口；查看器自己的模态窗口打开后则收起入口，避免穿透遮罩。
            launcher.hidden = true;
        }
        detailsHost.show();
        updateRegionSize();
        savePreferences();
    }

    /**
     * 取得框架主选项卡中当前 iframe 示例的窗口。
     * 内部调用 getActiveExample()；没有 PARAMS.mainTabStrip、活动项或 iframe 时返回空值。
     */
    function getActiveExample() {
        if (!window.PARAMS || !PARAMS.mainTabStrip) {
            return null;
        }
        var tabs = F(PARAMS.mainTabStrip);
        var activeTab = tabs && tabs.getActiveTab();
        return activeTab && activeTab.iframe ? activeTab.getIFrameWindow() : null;
    }

    /**
     * 打开框架当前活动示例的查看器；示例尚未就绪时记下待打开状态。
     * 作为 FineUISourceCodeViewer.open() 对外提供，主要用于框架入口；独立页按钮直接走 openDetails(window)。
     */
    function openActiveExample() {
        pendingOpen = true;
        var exampleWindow = getActiveExample();
        if (exampleWindow) {
            openDetails(exampleWindow);
        }
    }

    /**
     * 详情已经打开或等待打开时，让它跟随框架的新活动示例。
     * 绑定到主选项卡 tabchange，也作为 FineUISourceCodeViewer.follow() 提供；详情关闭时不主动打开。
     */
    function followActiveExample() {
        if (pendingOpen || (detailsHost && !detailsHost.hidden)) {
            openActiveExample();
        }
    }

    /**
     * 给当前源码 iframe 建立与父查看器的连接，防止旧源码页继续操作新示例。
     * 源码页调用 parent.FineUISourceCodeViewer.connect(window)；校验实际 iframe 后返回描述、标题、方向与关闭操作，否则返回 null。
     * 连接的 initializeControlReferences(content) 只在 Region 中按需启用控件定位，并再次核对连接是否仍有效。
     */
    function connectViewer(viewerWindow) {
        if (!detailsHost || detailsHost.getIFrameWindow() !== viewerWindow || !currentExample) {
            return null;
        }
        var description = currentExample.document.getElementById('example-description');
        var connectedExample = currentExample;
        return {
            description: description ? description.content : null,
            title: currentExample.document.title,
            regionMode: regionMode,
            getPosition: getPosition,
            switchPosition: switchPosition,
            initializeControlReferences: function (content) {
                if (!regionMode || !content.querySelector('[data-example-control]')) {
                    return;
                }
                loadControlReferenceModule().then(function () {
                    if (currentExample !== connectedExample || content.ownerDocument !== detailsHost.getIFrameWindow().document) {
                        return;
                    }
                    if (referenceController) {
                        referenceController.dispose();
                    }
                    referenceController = window.FineUISourceCodeControlLocator.initialize(content, {
                        getExampleWindow: function () {
                            return regionMode && currentExample === connectedExample && !detailsHost.hidden
                                ? connectedExample : null;
                        }
                    });
                }).catch(function () {
                    // 定位资源不可用时保留原生强调文字，页面描述仍可正常阅读。
                });
            },
            close: closeDetails
        };
    }

    /**
     * 只在 Region 的描述需要控件定位时加载定位脚本，并复用正在进行的加载。
     * 由连接内部调用，返回 Promise；失败时清除缓存，允许下次请求重试，Window 模式不触发该加载。
     */
    function loadControlReferenceModule() {
        // 只在 Region 查看器请求定位时加载；Window 描述使用原生 strong，不依赖这些资源。
        if (!referenceModulePromise) {
            referenceModulePromise = new Promise(function (resolve, reject) {
                var script = document.createElement('script');
                script.src = new URL('source-code-control-locator.js', viewerScriptUrl).href;
                script.addEventListener('load', resolve, { once: true });
                script.addEventListener('error', function () {
                    referenceModulePromise = null;
                    script.remove();
                    reject(new Error('控件引用资源加载失败。'));
                }, { once: true });
                document.head.appendChild(script);
            });
        }
        return referenceModulePromise;
    }

    /**
     * 从独立示例右下角按钮打开当前页的描述和源码窗口。
     * 作为自动创建的入口按钮 click 回调，调用 openDetails(window)。
     */
    function onOpenDetailsClick(event) {
        openDetails(window);
    }

    /**
     * 处理示例或框架中的 Escape，关闭本地查看器，或把关闭请求传给父框架。
     * 登记到 document.keydown；其他控件已经阻止默认行为时不接管，跨域父页面无法访问时保留页面行为。
     */
    function onKeyDown(event) {
        if (event.key !== 'Escape' || event.defaultPrevented) {
            return;
        }
        if (pendingOpen || (detailsHost && !detailsHost.hidden)) {
            closeDetails();
            return;
        }
        try {
            if (parent !== window && parent.FineUISourceCodeViewer) {
                parent.FineUISourceCodeViewer.closeFromExample(window);
            }
        } catch (error) {
            // 跨域父页面不能控制当前示例的查看器，保留本页原有键盘行为。
        }
    }

    /**
     * 接收实际示例发来的关闭请求，只关闭仍与该示例关联的查看器。
     * 子页可调用父框架 FineUISourceCodeViewer.closeFromExample(window)；手机预览跨多层 iframe 时继续向上转交。
     */
    function closeFromExample(exampleWindow) {
        if (currentExample === exampleWindow && detailsHost && !detailsHost.hidden) {
            closeDetails();
            return;
        }
        // 手机示例隔着预览页访问框架，逐层传递 Escape，不额外创建查看器。
        if (parent !== window && parent.FineUISourceCodeViewer) {
            parent.FineUISourceCodeViewer.closeFromExample(exampleWindow);
        }
    }

    whenPageReady(function () {
        // 原地址独立打开时才显示角落入口；站内 iframe 使用框架入口，辅助页不重复创建查看器。
        if (window === window.top && !(window.PARAMS && PARAMS.exampleWorkspace)) {
            var button = document.createElement('button');
            button.type = 'button';
            button.id = 'exampleDetailsLauncher';
            button.className = 'source-code-viewer-launcher';
            button.title = '查看页面描述和源代码';
            button.setAttribute('aria-label', '查看页面描述和源代码');
            var icon = document.createElement('i');
            icon.className = 'f-icon f-iconfont f-iconfont-code';
            icon.setAttribute('aria-hidden', 'true');
            button.appendChild(icon);
            button.addEventListener('click', onOpenDetailsClick);
            document.body.appendChild(button);
            launcher = button;
            var positionScript = document.createElement('script');
            positionScript.src = new URL('source-code-launcher.js', viewerScriptUrl).href;
            document.head.appendChild(positionScript);
        }
        var workspace = getWorkspace();
        if (workspace) {
            workspace.on('resize', onWorkspaceResize);
            F(PARAMS.mainTabStrip).on('tabchange', followActiveExample);
            F(PARAMS.exampleDetails).on('splitdrag', rememberSize);
            restorePreferences();
            if (pendingOpen) {
                openActiveExample();
            }
        }
        window.addEventListener('resize', onViewportResize);
        document.addEventListener('keydown', onKeyDown);
        // 传统 HTML 对照页不加载 FineUI 运行时；只在用户打开查看器时，用同源辅助页承载 F.Window。
        if (nativeSourceHost && parent !== window) {
            openDetails(parent);
        }
    });

    window.FineUISourceCodeViewer = {
        toggle: toggleDetails,
        open: openActiveExample,
        follow: followActiveExample,
        connect: connectViewer,
        closeFromExample: closeFromExample,
        rememberSize: rememberSize
    };
}());
