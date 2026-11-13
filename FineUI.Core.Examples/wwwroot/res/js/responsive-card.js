/**
 * 仪表盘和响应式表格示例共用的卡片排版，在窄屏卡片模式中显示头像、在校状态、专业、评分与简介。
 * 由 dashboard、block/grid_responsive 等页面引入，须先加载 grid.js，并配合 responsive-card.css。
 * 把 renderResponsiveCard 配置为 CardRendererFunction 或 cardRenderer；何时切换卡片模式由页面的响应式配置决定，本文件只负责内容。
 */

/**
 * 把一行学生数据组合为适合窄屏阅读的卡片，并复用表格的头像、状态、评分及专业渲染函数。
 * 作为 cardRenderer 使用；params.rowData.attrs 提供个人资料和简介，values 提供 AtSchool、Major、Rate1。
 */
function renderResponsiveCard(params) {
    var rowAttrs = params.rowData.attrs;
    var rowValues = params.rowData.values;

    /**
     * 为卡片生成一行带名称的字段，可放入 jQuery 元素、DOM 元素或已准备好的 HTML。
     * 仅由 renderResponsiveCard 调用，例如 fieldRow('评分：', renderRate1(value))；普通外部文字应先编码再传入。
     */
    function fieldRow(label, valueEl) {
        var valueWrap = $('<span>').addClass('responsive-card-value');
        if (valueEl instanceof jQuery || valueEl instanceof HTMLElement) {
            valueWrap.append(valueEl);
        } else {
            valueWrap.html(valueEl);
        }
        return $('<div>').addClass('responsive-card-row')
            .append($('<span>').addClass('responsive-card-label').text(label))
            .append(valueWrap);
    }

    // 头像区（复用 renderUserProfile），并把"在校/离校"插入到性别与入学年份之间，使顶部信息更紧凑
    var profileEl = renderUserProfile(null, params);
    var atSchool = rowValues['AtSchool'];
    var atSchoolEl = $('<span>').text(atSchool ? '在校' : '离校');
    if (!atSchool) {
        // 离校：删除线 + 警告色
        atSchoolEl.addClass('responsive-card-leaveschool');
    }
    profileEl.find('.desc').empty()
        .append(renderGender(rowAttrs['Gender']) + ' · ')
        .append(atSchoolEl)
        .append(' · 入学年份：' + rowAttrs['EntranceYear']);

    return $('<div>').addClass('responsive-card')
        .append($('<div>').addClass('responsive-card-top')
            .append(profileEl)
            .append(renderStatus(rowAttrs['Status'], params)))
        .append($('<div>').addClass('responsive-card-fields')
            .append(fieldRow('所学专业：', renderMajor(rowValues['Major'], params)))
            .append(fieldRow('评分：', renderRate1(rowValues['Rate1'])))
            .append(fieldRow('简介：', F.htmlEncode(rowAttrs['Desc'] || ''))));
}
