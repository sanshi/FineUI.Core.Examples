
function renderGender(value) {
    return value == 1 ? '男' : '女';
}

// https://fineui.com/js/api/global.html##F_Grid_cardRenderer
// params.rowData: 当前行数据
// params.renderer: 获取列渲染值的回调函数（如果列定义了渲染函数，通过此参数获取列渲染之后的值）
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


    // 创建成绩子元素
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
