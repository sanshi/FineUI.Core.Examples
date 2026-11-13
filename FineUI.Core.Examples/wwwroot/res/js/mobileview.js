/**
 * 让框架入口在窄屏下采用带遮罩的侧边菜单，恢复宽屏时切回常规布局。
 * 由需要移动视图的 Examples、QuickStart、AppBox 等入口在 index.js 之后加载，并配合 mobileview.css。
 * 依赖 index.js 的 PARAMS、侧栏宽度和折叠辅助函数；本文件接管同名 onFoldClick。
 * F.ready 自动检查初始宽度，之后通过 F.windowResize 响应尺寸变化；小于 992px 视为移动视图。
 */

/**
 * 判断当前窗口是否小于移动视图的 992px 分界。
 * 由折叠回调与尺寸检查调用 isSmallWindowWidth()，返回布尔值。
 */
function isSmallWindowWidth() {
    var windowWidth = $(window).width();
    return windowWidth < 992;
}

/**
 * 在宽窄屏切换时更新布局、侧栏状态和额外视口宽度，避免沿用上一模式的侧栏尺寸。
 * 由页面就绪和窗口 resize 调用；首次设置不启用过渡动画，之后启用 CSS 过渡。
 */
function checkMobileView() {
    var bodyEl = $('body');
    var isMobileView = bodyEl.hasClass('mobileview');

    if (isSmallWindowWidth()) {
        if (!isMobileView) {
            // 如果窗体宽度小于992px，但是尚未启用移动视图，则启用
            bodyEl.addClass('mobileview');
            F.viewPortExtraWidth = SIDEBAR_WIDTH_CONSTANT;
            
            // 更新为移动视图之前，要先展开左侧面板
            _sidebarWidth = SIDEBAR_WIDTH_CONSTANT;
            if (getFoldButtonStatus()) {
                // 如果当前处于折叠状态
                toggleSidebar(false);
            } else {
                // 如果当前处于展开状态，将左侧面板的宽度还原为初始值
                F(PARAMS.sidebarRegion).setWidth(_sidebarWidth);
            }
            setFoldButtonStatus(true);
            F(PARAMS.mainPanel).doLayout();
        }
    } else {
        if (isMobileView) {
            // 如果窗体宽度大于992px，但是已经启用移动视图，则禁用
            bodyEl.removeClass('mobileview');
            F.viewPortExtraWidth = 0;

            // 更新为正常视图之前，要先隐藏侧边栏和遮罩层
            hideMask();
            setFoldButtonStatus(false);
            F(PARAMS.mainPanel).doLayout();
        }
    }

    // 第一次检查时不启用动画
    if (!bodyEl.hasClass('mobileview-transition')) {
        bodyEl.addClass('mobileview-transition');
    }

}

/**
 * 收起移动侧边菜单并隐藏遮罩，同时把按钮更新为收起状态。
 * 移动视图内部调用 hideMask()，例如点击遮罩或选择叶菜单后调用。
 */
function hideMask() {
    $('.mainpanel').removeClass('showsidebar');
    $('.bodyregion .showsidebar-mask').hide();
    setFoldButtonStatus(true);
}

/**
 * 显示移动侧边菜单与遮罩，同时把按钮更新为展开状态。
 * 移动视图内部调用 showMask()；遮罩元素由首次点击折叠按钮时创建。
 */
function showMask() {
    $('.mainpanel').addClass('showsidebar');
    $('.bodyregion .showsidebar-mask').show();
    setFoldButtonStatus(false);
}



/**
 * 窄屏下显示或收起覆盖式侧边菜单，宽屏下继续切换常规侧栏。
 * 绑定为入口折叠按钮 click，替换 index.js 的同名回调；首次使用时创建遮罩并登记点击关闭。
 */
function onFoldClick(event) {
    if (isSmallWindowWidth()) {
        var sidebarregionEl = $('.sidebarregion');
        var bodyregionEl = $('.bodyregion');

        // 创建遮罩层
        var maskEl = bodyregionEl.find('.showsidebar-mask');
        if (!maskEl.length) {
            maskEl = $('<div class="showsidebar-mask"></div>').appendTo(bodyregionEl.find('>.f-panel-bodyct'));
            maskEl.on('click', function () {
                if (isSmallWindowWidth()) {
                    hideMask();
                }
            });

            sidebarregionEl.on('click', '.leftregion .f-tree-node-leaf', function () {
                if (isSmallWindowWidth()) {
                    hideMask();
                }
            });
        }

        if (getFoldButtonStatus()) {
            showMask();
        } else {
            hideMask();
        }

    } else {
        toggleSidebar();
    }
}


F.ready(function () {
    // 页面打开检查是否移动视图
    checkMobileView();

    // 根据屏幕尺寸检查是否移动视图
    F.windowResize(function () {
        checkMobileView();
    });

    // 如果初始启用移动视图，则检查折叠按钮的状态
    if (isSmallWindowWidth()) {
        setFoldButtonStatus(true);
    }
});