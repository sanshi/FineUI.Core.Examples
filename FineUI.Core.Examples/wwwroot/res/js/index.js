/**
 * 各类项目入口页共用的导航行为：连接左侧菜单与主选项卡，折叠侧边栏，刷新、打开或关闭业务页。
 * 由 Examples、EmptyProject、QuickStart 和 AppBox 的框架入口加载，依赖 FineUI、jQuery、入口页 PARAMS 及侧边栏宽度 CSS 变量。
 * PARAMS 保存当前页面真实控件 ID，子页可通过父框架调用 addExampleTab、activeTabAndRefresh 等函数。
 * F.ready 中只初始化一次菜单与选项卡；项目可用 configureIndexPage 和 onIndexPageReady 扩展该过程。
 */

// 获取后台定义的侧边栏宽度
var SIDEBAR_WIDTH_CONSTANT = parseInt($('html').css('--sidebar-width'), 10);
// 获取后台定义的侧边栏微型模式宽度
var SIDEBAR_MINIMODE_WIDTH_CONSTANT = parseInt($('html').css('--sidebar-minimode-width'), 10);

// _sidebarWidth变量会随着用户拖动分隔条而改变
var _sidebarWidth = SIDEBAR_WIDTH_CONSTANT;

/**
 * 打开入口页配置的主题选择窗口。
 * 绑定到主题入口的 click；窗口客户端 ID 由 PARAMS.windowThemeRoller 提供。
 */
function onThemeSelectClick(event) {
    F(PARAMS.windowThemeRoller).show();
}

/**
 * 切换桌面入口的侧边栏折叠状态。
 * 绑定到侧边栏折叠按钮；加载 mobileview.js 后，同名入口改由移动视图逻辑接管。
 */
function onFoldClick(event) {
    toggleSidebar();
}

/**
 * 让折叠按钮的图标与侧边栏状态一致，不单独改变侧边栏宽度。
 * 调用 setFoldButtonStatus(true) 表示已收起，false 表示已展开，通常由侧边栏或移动视图内部调用。
 */
function setFoldButtonStatus(collapsed) {
    var foldButton = F(PARAMS.btnCollapseSidebar);
    if (collapsed) {
        foldButton.setIconFont('f-iconfont-unfold');
    } else {
        foldButton.setIconFont('f-iconfont-fold');
    }
}

/**
 * 从折叠按钮当前图标读取是否处于收起状态。
 * 内部调用 getFoldButtonStatus()，返回布尔值，供 toggleSidebar 和移动视图判断下一步操作。
 */
function getFoldButtonStatus() {
    return F(PARAMS.btnCollapseSidebar).iconFont === 'f-iconfont-unfold';
}

/**
 * 展开侧边栏，恢复用户最近拖动后的宽度。
 * 页面脚本可直接调用 expandSidebar()，内部按展开状态调用 toggleSidebar。
 */
function expandSidebar() {
    toggleSidebar(false);
}

/**
 * 把侧边栏收为微型菜单，保留菜单导航入口。
 * 页面脚本可直接调用 collapseSidebar()，内部按折叠状态调用 toggleSidebar。
 */
function collapseSidebar() {
    toggleSidebar(true);
}

/**
 * 切换完整菜单与微型菜单，同时更新宽度、分隔条拖动和折叠图标。
 * toggleSidebar() 切换当前状态；传 true 明确收起，传 false 明确展开；重复设置同一状态不再重绘。
 */
function toggleSidebar(collapsed) {
    var sidebarRegion = F(PARAMS.sidebarRegion);
    var treeMenu = F(PARAMS.treeMenu);
    var sideheaderEl = $('#sideheader');
    var currentCollapsed = getFoldButtonStatus();

    if (F.isUND(collapsed)) {
        collapsed = !currentCollapsed;
    } else if (currentCollapsed === collapsed) {
        return;
    }

    F.noAnimation(function () {
        setFoldButtonStatus(collapsed);

        if (!collapsed) {
            sideheaderEl.removeClass('minimode');
            sidebarRegion.setWidth(_sidebarWidth);
            sidebarRegion.setSplitDraggable(true);
            treeMenu.miniMode = false;
            treeMenu.loadData();
        } else {
            sideheaderEl.addClass('minimode');
            sidebarRegion.setWidth(SIDEBAR_MINIMODE_WIDTH_CONSTANT);
            sidebarRegion.setSplitDraggable(false);
            treeMenu.miniMode = true;
            treeMenu.loadData();
        }
    });
}

/**
 * 记录侧边栏拖动后的宽度，使下次展开时恢复这个宽度。
 * 绑定到侧边 Region 的 splitdrag；this 是被拖动的侧边区域。
 */
function onSidebarSplitDrag(event) {
    _sidebarWidth = this.width;
}

/**
 * 刷新当前活动选项卡中的 iframe 页面。
 * 绑定到主选项卡标题栏的刷新工具；当前项不是 iframe 时不执行刷新。
 */
function onToolRefreshClick(event) {
    var activeTab = F(PARAMS.mainTabStrip).getActiveTab();
    if (activeTab.iframe) {
        activeTab.getIFrameWindow().location.reload();
    }
}

/**
 * 按菜单中的页面地址查找并打开主选项卡，复用菜单标题等配置。
 * 调用 addExampleTabByHref(href, actived)；href 为菜单地址，actived 交给 F.addMainTabByHref 决定是否激活。
 */
function addExampleTabByHref(href, actived) {
    F.addMainTabByHref(F(PARAMS.mainTabStrip), F(PARAMS.treeMenu), href, actived);
}

/**
 * 用一组明确的选项在主框架打开业务选项卡，并复用同 ID 的现有页面。
 * 推荐传对象，例如 addExampleTab({ id: 'orders', iframeUrl: '/orders', title: '订单' })。
 * 也兼容旧的位置参数 id、iframeUrl、title、icon、createToolbar、refreshWhenExist、iconFont；实际创建行为由 F.addMainTab 处理。
 */
function addExampleTab(tabOptions) {
    if (typeof (tabOptions) === 'string') {
        tabOptions = {
            id: arguments[0],
            iframeUrl: arguments[1],
            title: arguments[2],
            icon: arguments[3],
            createToolbar: arguments[4],
            refreshWhenExist: arguments[5],
            iconFont: arguments[6]
        };
    }
    F.addMainTab(F(PARAMS.mainTabStrip), tabOptions);
}

/**
 * 请求关闭当前活动选项卡，保留控件自身的关闭检查。
 * 由子页或框架脚本调用 removeActiveTab()；调用时须存在活动选项卡。
 */
function removeActiveTab() {
    F(PARAMS.mainTabStrip).getActiveTab().close();
}

/**
 * 取得当前活动选项卡 ID，便于子页保存来源或完成后返回。
 * 调用 getActiveTabId()；没有活动项时返回空字符串。
 */
function getActiveTabId() {
    var activeTab = F(PARAMS.mainTabStrip).getActiveTab();
    return activeTab ? activeTab.id : '';
}

/**
 * 返回指定选项卡、刷新其 iframe，再关闭原来的活动项。
 * 子页完成操作后调用 activeTabAndRefresh(tabId)；目标不存在时不执行，原活动项应是待关闭的另一个页签。
 */
function activeTabAndRefresh(tabId) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var targetTab = mainTabStrip.getTab(tabId);
    var oldActiveTab = mainTabStrip.getActiveTab();
    if (targetTab) {
        targetTab.activate();
        targetTab.refreshIFrame();
        oldActiveTab.close();
    }
}

/**
 * 返回指定选项卡，让其 iframe 执行 updatePage(param1)，再关闭原活动项。
 * 调用 activeTabAndUpdate(tabId, param1)；目标页须提供 updatePage 函数，原活动项应是待关闭的另一个页签。
 */
function activeTabAndUpdate(tabId, param1) {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var targetTab = mainTabStrip.getTab(tabId);
    var oldActiveTab = mainTabStrip.getActiveTab();
    if (targetTab) {
        targetTab.activate();
        targetTab.getIFrameWindow().updatePage(param1);
        oldActiveTab.close();
    }
}

/**
 * 在顶层页面中央显示三秒的信息通知。
 * 框架或子页脚本调用 notify(msg)；与 common.js 的遮罩通知用途不同，本函数不额外启用模态遮罩。
 */
function notify(msg) {
    F.notify({
        message: msg,
        messageIcon: 'information',
        target: '_top',
        header: false,
        displayMilliseconds: 3 * 1000,
        positionX: 'center',
        positionY: 'center'
    });
}

F.ready(function () {
    var mainTabStrip = F(PARAMS.mainTabStrip);
    var treeMenu = F(PARAMS.treeMenu);
    if (!treeMenu) return;

    var initOptions = {
        maxTabCount: 30,
        maxTabMessage: '请先关闭一些选项卡（最多允许打开 30 个）！',
        // 应用部署在子路径时，子路径只属于实际 HTTP 请求地址，Hash 只保存入口页内路由。
        compactHash: true,
        beforeTabAdd: function (event, tabOptions) {
            // 关闭时移除选项卡实例，避免入口页长期运行时不断积累已经关闭的实例。
            tabOptions.removeOnClose = true;
        }
    };

    // 项目专属脚本只扩展选项，不得再次调用 F.initTreeTabStrip。
    if (typeof configureIndexPage === 'function') {
        configureIndexPage(initOptions, treeMenu, mainTabStrip);
    }

    F.initTreeTabStrip(treeMenu, mainTabStrip, initOptions);

    if (typeof onIndexPageReady === 'function') {
        onIndexPageReady(initOptions, treeMenu, mainTabStrip);
    }
});
