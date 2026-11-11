(function () {
    'use strict';

    // 纯 JavaScript 示例异步加载此资源；若页面已经就绪，立即初始化，避免入口和恢复监听缺失。
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
        }
        document.title = '页面描述和源代码 · ' + connection.title;
        document.addEventListener('keydown', onSourcePageKeyDown);

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

        function updatePositionTool() {
            var isBottom = connection.getPosition() === 'bottom';
            var label = isBottom ? '移到右侧' : '移到下方';
            var tool = F('toolPositionDetails');
            tool.setIconFont(isBottom ? 'f-iconfont-right' : 'f-iconfont-down');
            tool.setTooltip(label);
            tool.el.attr('aria-label', label);
        }

        function onPositionClick(event) {
            connection.switchPosition();
            updatePositionTool();
        }
    }

    function onSourceFileKeyDown(event) {
        if (event.key === 'Escape' && parent !== window && parent.FineUISourceCodeViewer) {
            parent.FineUISourceCodeViewer.close();
        }
    }

    var sourceAddress = document.querySelector('meta[name="fineui-example-source"]');
    if (!sourceAddress) {
        return;
    }

    var detailsHost;
    var windowHost;
    var regionMode = false;
    var currentExample;
    var currentAddress;
    var watchedFrame;
    var preferredWidth = 480;
    var preferredHeight = 360;
    var regionPosition = 'bottom';
    var returnFocus;
    var sizeUpdateFrame;
    var pendingOpen = false;
    var preferenceKey = 'FineUI.SourceCodeViewer.' + sourceAddress.content;

    function needsDescriptionTab(exampleWindow) {
        var description = exampleWindow.document.getElementById('example-description');
        return description && description.content.textContent.trim().length > 0;
    }

    function getWorkspace() {
        if (window.PARAMS && PARAMS.exampleWorkspace) {
            return F(PARAMS.exampleWorkspace);
        }
        return null;
    }

    function readPreferredSize(value) {
        return typeof value === 'number' && Number.isFinite(value) && value >= 1 && value <= 8192
            ? Math.round(value)
            : undefined;
    }

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

    function restoreFocus() {
        if (returnFocus && returnFocus.isConnected) {
            returnFocus.focus();
        }
    }

    function closeDetails() {
        pendingOpen = false;
        if (detailsHost) {
            detailsHost.hide();
        }
        savePreferences();
        restoreFocus();
    }

    function toggleDetails() {
        if (pendingOpen || (detailsHost && !detailsHost.hidden)) {
            closeDetails();
        } else {
            openActiveExample();
        }
    }

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
                    savePreferences();
                    restoreFocus();
                }
            }
        });
    }

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

    function onWorkspaceResize(event) {
        if (sizeUpdateFrame) {
            window.cancelAnimationFrame(sizeUpdateFrame);
        }
        sizeUpdateFrame = window.requestAnimationFrame(function () {
            sizeUpdateFrame = undefined;
            updateRegionSize();
        });
    }

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

    function switchPosition() {
        if (regionMode && detailsHost) {
            regionPosition = regionPosition === 'bottom' ? 'right' : 'bottom';
            detailsHost.setRegionPosition(regionPosition);
            updateRegionSize();
            savePreferences();
        }
        return regionPosition;
    }

    function getPosition() {
        return regionPosition;
    }

    function onExampleFrameLoad(event) {
        if ((pendingOpen || (detailsHost && !detailsHost.hidden)) && watchedFrame) {
            openDetails(watchedFrame.contentWindow);
        }
    }

    function watchExample(exampleWindow) {
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

    function openDetails(exampleWindow) {
        var address;
        try {
            // 新页签刚激活时可能仍是 about:blank，先监听加载，实际示例就绪后再更新详情。
            watchExample(exampleWindow);
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

        // 同一示例只显示已有区域或窗口，不重新加载源码，也不移动主演示 iframe。
        if (currentAddress !== viewerAddress) {
            currentAddress = viewerAddress;
            detailsHost.setIFrameUrl(viewerAddress);
        }
        detailsHost.show();
        updateRegionSize();
        savePreferences();
    }

    function getActiveExample() {
        if (!window.PARAMS || !PARAMS.mainTabStrip) {
            return null;
        }
        var tabs = F(PARAMS.mainTabStrip);
        var activeTab = tabs && tabs.getActiveTab();
        return activeTab && activeTab.iframe ? activeTab.getIFrameWindow() : null;
    }

    function openActiveExample() {
        pendingOpen = true;
        var exampleWindow = getActiveExample();
        if (exampleWindow) {
            openDetails(exampleWindow);
        }
    }

    function followActiveExample() {
        if (pendingOpen || (detailsHost && !detailsHost.hidden)) {
            openActiveExample();
        }
    }

    function connectViewer(viewerWindow) {
        if (!detailsHost || detailsHost.getIFrameWindow() !== viewerWindow || !currentExample) {
            return null;
        }
        var description = currentExample.document.getElementById('example-description');
        return {
            description: description ? description.content : null,
            title: currentExample.document.title,
            regionMode: regionMode,
            getPosition: getPosition,
            switchPosition: switchPosition,
            close: closeDetails
        };
    }

    function onOpenDetailsClick(event) {
        openDetails(window);
    }

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

    function closeFromExample(exampleWindow) {
        if (currentExample === exampleWindow && detailsHost && !detailsHost.hidden) {
            closeDetails();
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
