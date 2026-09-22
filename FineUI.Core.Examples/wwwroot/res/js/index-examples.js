// Examples 入口页专属脚本：搜索、源码查看、示例首页和演示偏好不进入其他项目。

var _mainTabs = F.cookie('MainTabs') || 'multi';

// 设置长期存在的 Cookie
function setCookie(name, value) {
    F.cookie(name, value, {
        expires: 100
    });
}

// 社区版下载
function onBaseDownloadClick(event) {
    this.setBadge(false);
    window.open('http://fineui.com/fans/', '_blank');
}

// 点击企业版试用
function onApplyTrialClick(event) {
    F(PARAMS.windowApplyTrial).show();
}

// 点击加载动画
function onLoadingSelectClick(event) {
    F(PARAMS.windowLoadingSelector).show();
}

// 点击仅显示社区版示例
function onShowOnlyCommunityClick(event) {
    setCookie('ShowOnlyCommunity', this.isChecked());
    top.window.location.reload();
}

function onSearchTrigger1Click(event) {
    F.removeCookie('SearchText');
    top.window.location.reload();
}

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

// 点击标题栏工具图标 - 查看源代码
function onToolSourceCodeClick(event) {
    var activeTab = F(PARAMS.mainTabStrip).getActiveTab();
    var iframeWnd;
    var iframeUrl;
    if (activeTab.iframe) {
        iframeWnd = activeTab.getIFrameWindow();
        iframeUrl = activeTab.getIFrameUrl();
    }

    var files = [iframeUrl];
    var sourcefilesNode = $(iframeWnd.document).find('head meta[name=sourcefiles]');
    if (sourcefilesNode.length) {
        $.merge(files, sourcefilesNode.attr('content').split(';'));
    }
    F(PARAMS.windowSourceCode).show(PARAMS.sourceUrl + '?files=' + encodeURIComponent(files.join(';')));
}

// 点击标题栏工具图标 - 在新标签页中打开
function onToolNewWindowClick(event) {
    var activeTab = F(PARAMS.mainTabStrip).getActiveTab();
    if (activeTab.iframe) {
        window.open(PARAMS.processNewWindowUrl(activeTab.getIFrameUrl()), '_blank');
    }
}

// 更新首页的入口页风格
function updateFramePageStyle(indexStyle) {
    $('body').removeClass('f-dark-top f-dark-left f-dark-top-left').addClass(indexStyle);
}

// 点击入口页风格
function onMenuFramePageStyleCheckChange(event, item, checked) {
    var indexStyle = item.getAttr('data-tag');
    setCookie('FramePageStyle', indexStyle);
    updateFramePageStyle(indexStyle);
}

// 点击显示模式
function onMenuDisplayModeCheckChange(event, item, checked) {
    setCookie('DisplayMode', item.getAttr('data-tag'));
    top.window.location.reload();
}

// 点击语言
function onMenuLangCheckChange(event, item, checked) {
    setCookie('Language', item.getAttr('data-tag'));
    top.window.location.reload();
}

// 点击选项卡标签页模式
function onMenuMainTabsCheckChange(event, item, checked) {
    setCookie('MainTabs', item.getAttr('data-tag'));
    top.window.location.reload();
}

// 示例数
function getExamplesCount() {
    var hfExamplesCount = F(PARAMS.hfExamplesCount);
    return hfExamplesCount ? hfExamplesCount.getValue() : F(PARAMS.treeMenu).getNodeCount(true);
}

var THEMES = ["Pure_Black", "Pure_Green", "Pure_Blue", "Pure_Purple", "Pure_Orange", "Pure_Red", "Default", "Metro_Blue", "Metro_Dark_Blue", "Metro_Gray", "Metro_Green", "Metro_Orange", "Black_Tie", "Blitzer", "Cupertino", "Dark_Hive", "Dot_Luv", "Eggplant", "Excite_Bike", "Flick", "Hot_Sneaks", "Humanity", "Le_Frog", "Mint_Choc", "Overcast", "Pepper_Grinder", "Redmond", "Smoothness", "South_Street", "Start", "Sunny", "Swanky_Purse", "Trontastic", "UI_Darkness", "UI_Lightness", "Vader", "custom_default", "image_black_sky", "image_green_rain", "image_green_drip", "image_green_poppy", "image_green_lotus", "image_blue_sky", "image_blue_star", "image_blue_moon", "image_blue_drip", "image_purple_fog", "image_orange_light", "image_red_dawn"];

// 转到下一个主题
function nextThemePlease() {
    var currentThemeIndex = $.inArray(F.cookie('Theme'), THEMES);
    if (currentThemeIndex === -1) return;
    currentThemeIndex++;
    if (currentThemeIndex >= THEMES.length) currentThemeIndex = 0;
    setCookie('Theme', THEMES[currentThemeIndex]);
    window.location.reload();
}

function updateBreadcrumb(treeInstance, treeNodeId) {
    $('#breadcrumb .breadcrumb-inner').html(generateBreadcrumbHtml(treeInstance, treeNodeId));
}

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

// 扩展 index.js 创建的初始化选项；这里只修改同一个初始化过程，不重复初始化控件。
function configureIndexPage(initOptions, treeMenu, mainTabStrip) {
    updateFramePageStyle(F.cookie('FramePageStyle'));

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
