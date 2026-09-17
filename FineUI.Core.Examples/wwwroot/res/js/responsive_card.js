
// 响应式卡片渲染函数（表格在小屏切换为卡片模式时使用）
// dashboard 和 block/grid_responsive 两个页面共用
// 依赖 grid.js 中的 renderUserProfile / renderStatus / renderRate1 / renderMajor
function renderResponsiveCard(params) {
    var rowAttrs = params.rowData.attrs;
    var rowValues = params.rowData.values;

    // 卡片字段行辅助函数
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
