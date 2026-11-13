/**
 * 表格卡片模式示例的共用渲染函数，把同一行记录显示为字段卡片，或带头像、状态和成绩的资料卡片。
 * 由 GridCard 下的示例按需引入，并配合页面中的卡片样式。
 * 通过 CardRendererFunction="renderCard"、CardRendererFunction="renderCardWithAvatar" 或对应的 cardRenderer 配置使用，框架逐行传入 params。
 * 头像卡片的查看详情链接仅演示携带用户编号，示例项目没有提供对应的业务详情页。
 */

/**
 * 为卡片示例的性别列把数值 1 显示为男，其余显示为女。
 * 配置为该列的渲染函数，renderCard 可通过 params.renderer 取得这个显示结果。
 */
function renderGender(value) {
    return value == 1 ? '男' : '女';
}

/**
 * 按表格现有列定义生成编号、姓名、日期等字段卡片，复用各列已配置的格式化结果。
 * 作为 cardRenderer 或 CardRendererFunction 使用；params.rowData 是当前行，params.renderer(columnId) 取得对应列显示内容。
 * 参数参考：https://fineui.com/js/api/global.html##F_Grid_cardRenderer
 */
function renderCard(params) {
    var rowId = params.rowData.id;
    var renderer = params.renderer;

    // 是否在校（图标）
    var atSchool = params.rowData.values['AtSchool'] ? '<i class="f-icon f-iconfont f-grid-static-checkbox f-checked"></i>' : '<i class="f-icon f-iconfont f-grid-static-checkbox"></i>';

    return $('<table>')
        .append($('<tr>').append($('<td>').text('编号：')).append($('<td>').text(rowId)))
        .append($('<tr>').append($('<td>').text('姓名：')).append($('<td>').text(renderer('Name'))))
        .append($('<tr>').append($('<td>').text('性别：')).append($('<td>').text(renderer('Gender'))))
        .append($('<tr>').append($('<td>').text('入学年份：')).append($('<td>').text(renderer('EntranceYear'))))
        .append($('<tr>').append($('<td>').text('入学日期：')).append($('<td>').text(renderer('EntranceDate'))))
        .append($('<tr>').append($('<td>').text('是否在校：')).append($('<td>').html(atSchool)))
        .append($('<tr>').append($('<td>').text('所学专业：')).append($('<td>').html(renderer('Major'))));
}


/**
 * 生成包含头像、状态徽章、语文、数学和总分的学生资料卡片。
 * 作为卡片渲染函数；行 attrs 提供 Avatar、Name、Status、EntranceYear，values 提供 Major、ChineseScore、MathScore、TotalScore。
 */
function renderCardWithAvatar(params) {
    var rowId = params.rowData.id;

    var rowAttrs = params.rowData.attrs;
    var rowValues = params.rowData.values;

    // 根据状态值获取徽章类型和文本
    var badgeType, badgeText;
    switch (rowAttrs['Status']) {
        case 1:
            badgeType = 'success';
            badgeText = '优秀';
            break;
        case 2:
            badgeType = 'processing';
            badgeText = '良好';
            break;
        case 3:
            badgeType = 'warning';
            badgeText = '补考';
            break;
        case 4:
            badgeType = 'error';
            badgeText = '重修';
            break;
    }

    // 添加动画效果
    var badgeEl = $('<div>').addClass('f-badge');
    if (badgeType === 'success') {
        badgeEl.addClass('f-badge-animation-processing');
    } else if (badgeType === 'error') {
        badgeEl.addClass('f-badge-animation-fade');
    }

    // 创建头像元素
    var avatarEl = $('<div>').addClass('avatar')
        .append($('<img>').attr('src', F.resolveUrl(rowAttrs['Avatar'])))
        .append($('<div>').addClass('f-badge-ct f-badge-type-' + badgeType)
            .append(badgeEl));


    /**
     * 为当前卡片生成一个带科目样式的成绩块。
     * 仅在头像卡片内部调用，例如 createScoreEl(rowValues.ChineseScore, 'chinese')；scoreType 同时决定样式与标题。
     */
    function createScoreEl(scoreValue, scoreType) {
        var scoreTitle = '';
        if (scoreType === 'chinese') {
            scoreTitle = '语文';
        } else if (scoreType === 'math') {
            scoreTitle = '数学';
        } else {
            scoreTitle = '总分';
        }

        return $('<div>').addClass('score-item').addClass(scoreType.toLowerCase())
            .append($('<span>').addClass('score-value').text(scoreValue))
            .append($('<span>').addClass('score-title').text(scoreTitle));
    }

    // 创建用户卡片元素
    var userProfileEl = $('<div>')
        .addClass('user-profile')
        .append(avatarEl)
        .append($('<div>').addClass('name').text(rowAttrs['Name']))
        .append($('<div>').addClass('desc').text(rowValues['Major'] + ' · ' + rowAttrs['EntranceYear'] + '级' + ' · ' + badgeText))
        .append($('<div>').addClass('score')
            .append(createScoreEl(rowValues['ChineseScore'], 'chinese'))
            .append(createScoreEl(rowValues['MathScore'], 'math'))
            .append(createScoreEl(rowValues['TotalScore'], 'total')));

    // 示例只展示携带用户编号的详情链接，不提供对应页面。
    var detailsUrl = F.resolveUrl('/user-details?id=' + encodeURIComponent(rowId));
    var detailsLinkEl = $('<a>').addClass('user-details-link').attr('href', detailsUrl).text('查看详情');
    var userActionsEl = $('<div>').addClass('user-actions')
        .append(detailsLinkEl);

    return $('<div>')
        .addClass('user-card')
        .append(userProfileEl)
        .append(userActionsEl);
}
