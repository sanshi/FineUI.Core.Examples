/**
 * 官方示例入口专用功能：搜索菜单、打开页面描述和源码、切换展示偏好、更新面包屑及默认首页。
 * 由 Examples 首页与 index.js 配合加载，依赖入口页 PARAMS、菜单树、主选项卡及对应弹窗。
 * 按钮和菜单绑定本文件的具名回调；index.js 在唯一的初始化过程中调用 configureIndexPage 和 onIndexPageReady。
 * 用户偏好通过 Cookie 保存；源码工具调用 source-code-viewer.js 提供的 FineUISourceCodeViewer。
 */

var _mainTabs = F.cookie('MainTabs') || 'multi';

/**
 * 保存示例入口的长期偏好，使重载后继续采用同一设置。
 * 内部调用 setCookie(name, value)，统一保存 100 天；框架业务数据不存入这些偏好。
 */
function setCookie(name, value) {
    F.cookie(name, value, {
        expires: 100
    });
}

/**
 * 打开社区版下载入口，并清除当前按钮的徽标提示。
 * 绑定到下载按钮 click；this 是被点击的按钮。
 */
function onBaseDownloadClick(event) {
    this.setBadge(false);
    window.open('http://fineui.com/fans/', '_blank');
}

/**
 * 打开企业版试用窗口。
 * 绑定到试用入口 click，窗口客户端 ID 来自 PARAMS.windowApplyTrial。
 */
function onApplyTrialClick(event) {
    F(PARAMS.windowApplyTrial).show();
}

/**
 * 打开加载动画选择窗口。
 * 绑定到加载动画入口 click，窗口客户端 ID 来自 PARAMS.windowLoadingSelector。
 */
function onLoadingSelectClick(event) {
    F(PARAMS.windowLoadingSelector).show();
}

/**
 * 保存是否只显示社区版示例，并重载入口页重新生成菜单。
 * 绑定到相应复选菜单项的 click；从 this.isChecked() 读取选择。
 */
function onShowOnlyCommunityClick(event) {
    setCookie('ShowOnlyCommunity', this.isChecked());
    top.window.location.reload();
}

/**
 * 清除已保存的菜单搜索条件，并重载显示完整菜单。
 * 绑定到搜索框的清除触发器；也由空搜索提交时调用。
 */
function onSearchTrigger1Click(event) {
    F.removeCookie('SearchText');
    top.window.location.reload();
}

/**
 * 首次点击展开搜索框，再次提交非空关键词后重载筛选菜单。
 * 绑定到搜索触发器；this 必须是搜索框，提交空值时会按当前偏好决定是否清除筛选。
 */
function onSearchTrigger2Click(event) {
    var ttbxSearch = this;
    if (ttbxSearch.el.hasClass('collapsed')) {
        ttbxSearch.el.removeClass('collapsed').addClass('expanded').outerWidth(200);
    } else {
        var searchText = ttbxSearch.getValue();
        if (searchText) {
            setCookie('SearchText', searchText);
            top.window.location.reload();
        } else if (F.cookie('SearchText')) {
            onSearchTrigger1Click(event);
        }
    }
}

/**
 * 搜索框失焦时恢复已提交的关键词，或把无内容的搜索框收为图标。
 * 绑定到搜索框 blur；只调整当前框的外观和值，不重新筛选菜单。
 */
function onSearchBlur(event) {
    var ttbxSearch = this;
    if (ttbxSearch.getValue()) return;

    var searchText = F.cookie('SearchText');
    if (searchText) {
        ttbxSearch.setValue(searchText);
    } else {
        ttbxSearch.el.removeClass('expanded').addClass('collapsed').outerWidth(24);
    }
}

/**
 * 切换当前示例的页面描述和源代码区域。
 * 绑定到主选项卡的源码工具 click；通过 FineUISourceCodeViewer.toggle() 交给查看器处理承载方式。
 */
function onToolSourceCodeClick(event) {
    if (window.FineUISourceCodeViewer) {
        window.FineUISourceCodeViewer.toggle();
    }
}

/**
 * 把当前 iframe 示例的地址交给浏览器新标签页打开。
 * 绑定到在新窗口打开工具；PARAMS.processNewWindowUrl 负责把框架内地址转换为独立页地址。
 */
function onToolNewWindowClick(event) {
    var activeTab = F(PARAMS.mainTabStrip).getActiveTab();
    if (activeTab.iframe) {
        window.open(PARAMS.processNewWindowUrl(activeTab.getIFrameUrl()), '_blank');
    }
}

/**
 * 根据入口页风格更新顶部和侧栏的深色样式类。
 * 内部调用 updateFramePageStyle(indexStyle)，indexStyle 是菜单 data-tag 或 Cookie 中保存的样式类。
 */
function updateFramePageStyle(indexStyle) {
    $('body').removeClass('f-dark-top f-dark-left f-dark-top-left').addClass(indexStyle);
}

/**
 * 保存并立即应用入口页风格，不重载当前示例。
 * 绑定到风格菜单的 checkchange；item 是发生变化的菜单项，data-tag 提供样式类。
 */
function onMenuFramePageStyleCheckChange(event, item, checked) {
    var indexStyle = item.getAttr('data-tag');
    setCookie('FramePageStyle', indexStyle);
    updateFramePageStyle(indexStyle);
}

/**
 * 保存显示模式并重载入口页，使新模式参与控件创建。
 * 绑定到显示模式菜单的 checkchange，取 item 的 data-tag。
 */
function onMenuDisplayModeCheckChange(event, item, checked) {
    setCookie('DisplayMode', item.getAttr('data-tag'));
    top.window.location.reload();
}

/**
 * 保存语言选择并重载入口页，重新加载相应文字资源。
 * 绑定到语言菜单的 checkchange，取 item 的 data-tag。
 */
function onMenuLangCheckChange(event, item, checked) {
    setCookie('Language', item.getAttr('data-tag'));
    top.window.location.reload();
}

/**
 * 保存单标签或多标签模式，重载后由初始化选项应用。
 * 绑定到主标签模式菜单的 checkchange，取 item 的 data-tag。
 */
function onMenuMainTabsCheckChange(event, item, checked) {
    setCookie('MainTabs', item.getAttr('data-tag'));
    top.window.location.reload();
}

/**
 * 读取当前入口可展示的示例数量，供首页统计使用。
 * 调用 getExamplesCount()；优先使用后台数量控件，否则统计菜单树叶节点。
 */
function getExamplesCount() {
    var hfExamplesCount = F(PARAMS.hfExamplesCount);
    return hfExamplesCount ? hfExamplesCount.getValue() : F(PARAMS.treeMenu).getNodeCount(true);
}

var THEMES = ["Pure_Black", "Pure_Green", "Pure_Blue", "Pure_Purple", "Pure_Orange", "Pure_Red", "Default", "Metro_Blue", "Metro_Dark_Blue", "Metro_Gray", "Metro_Green", "Metro_Orange", "Black_Tie", "Blitzer", "Cupertino", "Dark_Hive", "Dot_Luv", "Eggplant", "Excite_Bike", "Flick", "Hot_Sneaks", "Humanity", "Le_Frog", "Mint_Choc", "Overcast", "Pepper_Grinder", "Redmond", "Smoothness", "South_Street", "Start", "Sunny", "Swanky_Purse", "Trontastic", "UI_Darkness", "UI_Lightness", "Vader", "custom_default", "image_black_sky", "image_green_rain", "image_green_drip", "image_green_poppy", "image_green_lotus", "image_blue_sky", "image_blue_star", "image_blue_moon", "image_blue_drip", "image_purple_fog", "image_orange_light", "image_red_dawn"];

/**
 * 按预设列表切换到下一个主题，并重载页面。
 * 由入口的 Shift+L 快捷键调用，也可直接调用 nextThemePlease()；当前主题不在列表中时不切换。
 */
function nextThemePlease() {
    var currentThemeIndex = $.inArray(F.cookie('Theme'), THEMES);
    if (currentThemeIndex === -1) return;
    currentThemeIndex++;
    if (currentThemeIndex >= THEMES.length) currentThemeIndex = 0;
    setCookie('Theme', THEMES[currentThemeIndex]);
    window.location.reload();
}

/**
 * 把指定菜单节点的完整路径显示为入口页面包屑。
 * 由标签添加和切换回调调用，传入菜单树实例与节点 ID。
 */
function updateBreadcrumb(treeInstance, treeNodeId) {
    $('#breadcrumb .breadcrumb-inner').html(generateBreadcrumbHtml(treeInstance, treeNodeId));
}

/**
 * 根据菜单路径生成面包屑文字，跳过内部根节点并标明最后一项。
 * 供 updateBreadcrumb(treeInstance, nodeId) 调用；节点或路径不存在时返回空字符串，菜单文字来自示例清单。
 */
function generateBreadcrumbHtml(treeInstance, nodeId) {
    var result = [];
    if (!nodeId) return '';

    var nodePathString = treeInstance.getNodePath(nodeId);
    if (!nodePathString) return '';

    var nodePaths = nodePathString.split('/');
    var nodePathLength = nodePaths.length;
    $.each(nodePaths, function (index, item) {
        if (item === 'root') return;
        var cls = 'breadcrumb-text';
        if (index === nodePathLength - 1) cls += ' breadcrumb-last';
        result.push('<span class="' + cls + '">' + treeInstance.getNodeData(item).text + '</span>');
    });
    return result.join('<span class="breadcrumb-separator">/</span>');
}

/**
 * 在共用入口的同一次初始化中增加示例专属菜单行为、面包屑和单标签模式。
 * 由 index.js 调用 configureIndexPage(initOptions, treeMenu, mainTabStrip)，修改传入选项，不再次调用 F.initTreeTabStrip。
 */
function configureIndexPage(initOptions, treeMenu, mainTabStrip) {
    updateFramePageStyle(F.cookie('FramePageStyle'));

    /**
     * 为主题窗口及独立打开的菜单项提供特殊处理，其他项继续走正常选项卡导航。
     * 由菜单初始化器调用，传 event 和 treeNodeId；返回 false 表示已处理本次菜单点击。
     */
    initOptions.beforeNodeClick = function (event, treeNodeId) {
        var nodeEl = treeMenu.getNodeEl(treeNodeId);
        var nodeTag = nodeEl.attr('data-tag');
        var nodeData = treeMenu.getNodeData(treeNodeId);
        if (nodeTag === 'pop-window1') {
            F(PARAMS.windowThemeRoller).show();
            return false;
        } else if (nodeTag === 'newtab') {
            window.open(nodeData.href, '_blank');
            return false;
        }
    };

    var commonBeforeTabAdd = initOptions.beforeTabAdd;
    /**
     * 保留共用的关闭清理规则，再为特殊菜单补充标签标题和面包屑。
     * 由标签创建过程调用；先执行 index.js 提供的原回调，treeNodeId 为空时不读取菜单数据。
     */
    initOptions.beforeTabAdd = function (event, tabOptions, treeNodeId) {
        commonBeforeTabAdd(event, tabOptions, treeNodeId);
        if (!treeNodeId) return;

        var nodeEl = treeMenu.getNodeEl(treeNodeId);
        if (nodeEl.attr('data-tag') === 'custom-title') {
            var parentNode = treeMenu.getParentData(treeNodeId);
            var customTitle = parentNode.text + ' - ' + nodeEl.text();
            tabOptions.title = parentNode.textRaw ? F.rawHtml(customTitle) : customTitle;
        }
        updateBreadcrumb(treeMenu, treeNodeId);
    };
    /**
     * 切换标签后用关联菜单节点更新面包屑。
     * 作为初始化选项交给菜单与选项卡联动过程，参数包含 event、tabInstance、treeNodeId。
     */
    initOptions.tabChange = function (event, tabInstance, treeNodeId) {
        updateBreadcrumb(treeMenu, treeNodeId);
    };

    if (_mainTabs === 'single') {
        $('body').addClass('maintabs-single');
        $.extend(initOptions, {
            singleTabId: PARAMS.mainTabStrip + '_singleTab',
            refreshWhenExist: true
        });
    }
}

/**
 * 菜单和主选项卡完成初始化后，按地址打开默认首页，并登记主题快捷键。
 * 由 index.js 调用；已有其他示例路由时保留该路由，compactHash 模式会去除部署子路径再比较首页地址。
 */
function onIndexPageReady(initOptions, treeMenu, mainTabStrip) {
    var hashFragment = window.location.hash.substr(1);
    var mainHashUrl = PARAMS.mainUrl;
    if (initOptions.compactHash) {
        var baseUrl = (F.baseUrl || '').replace(/\/+$/, '');
        if (baseUrl && baseUrl !== '/' && mainHashUrl.indexOf(baseUrl + '/') === 0) {
            mainHashUrl = mainHashUrl.substring(baseUrl.length);
        }
    }
    if (!hashFragment || hashFragment.indexOf(mainHashUrl) >= 0) {
        addExampleTabByHref(PARAMS.dashboardUrl);
    }

    $(document).on('keydown', function (event) {
        if (event.shiftKey && event.keyCode === 76) nextThemePlease();
    });
}
