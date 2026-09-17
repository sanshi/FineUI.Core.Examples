
// 多语言支持
window._R = F.getResource;
// 初始化多语言资源
F.setResources({
    "Male": "男",
    "Female": "女",
    "NamePrefix": "姓名：",
    "IntroPrefix": "简介：",
    "NoSelectionMessage": "没有选中项！",
    "GridRowId": "行标识",
    "GridRowText": "行文本",
    "GridGender": "性别",
    "GridMajor": "专业"
});

// 表格列 - 用户状态
function renderStatus(value, params) {
    value = parseInt(value, 10);

    var badgeType, badgeText;
    switch (value) {
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

    return $('<div>')
        .addClass('f-badge-tag')
        .addClass('f-badge-type-' + badgeType)
        .append(badgeEl)
        .append($('<span>').text(badgeText));
}

// 表格列 - 用户基本信息
function renderUserProfile(value, params) {
    var rowAttrs = params.rowData.attrs;
    var desc = renderGender(rowAttrs['Gender']) + ' · 入学年份：' + rowAttrs['EntranceYear'];

    return $('<div>')
        .addClass('user-profile')
        .append($('<div>').addClass('avatar')
            .append($('<img>').attr('src', F.resolveUrl(rowAttrs['Avatar']))))
        .append($('<div>').addClass('name-desc')
            .append($('<div>').addClass('name').text(rowAttrs['Name']))
            .append($('<div>').addClass('desc').text(desc)));
}

// 表格列 - 评分
function renderRate1(value) {
    return F.rateHtml(value, {
        readonly: true,
        iconFont: 'f-iconfont-thumbs-up'
    });
}



// 文本 - 性别
function renderGender(value, params) {
    return (value == 1 || value === '男') ? _R('Male') : _R('Female');
}

// 超链接标签 - 所学专业
function renderMajor(value, params) {
    if (value === '') {
        return ''; // 如果专业值为空，则不显示超链接
    }
    var encodedValue = F.htmlEncode(value);
    return $('<a>', {
        'target': '_blank',
        'href': 'http://gsa.ustc.edu.cn/search?q=' + F.urlEncode(value),
        'data-qtip': encodedValue,
        'html': encodedValue
    });
}

// 图片标签 - 分组
function renderGroup(value, params) {
    if (value === '') {
        return ''; // 如果分组值为空，则不显示图标
    }
    return $('<img>', {
        'class': 'f-grid-imagefield',
        'alt': 'Group',
        'src': F.resolveUrl('/res/images/16/' + value + '.png')
    });
}

// HTML - 行扩展列
function renderExpander(value, params) {
    var rowData = params.rowData;
    return $('<div>').addClass('expander')
        .append($('<p>').html('<strong>' + _R('NamePrefix') + '</strong>' + rowData.values['Name']))
        .append($('<p>').html('<strong>' + _R('IntroPrefix') + '</strong>' + value));
}

// 超链接标签 - 删除图标
function renderDeleteAction(value, params) {
    return $('<a>').addClass('action-btn delete').attr('href', 'javascript:;')
        .append($('<img>').addClass('f-grid-cell-icon').attr('src', F.resolveUrl('/res/icon/delete.png')).attr('alt', 'Delete'));
}


// 公共方法 - 显示通知框
function showNotify(content) {
    // 消息正文可能会比较长，所以不显示前面的图标（messageIcon: ''）
    F.notify({
        message: content,
        target: '_top',
        header: false,
        messageIcon: '',
        positionX: 'center',
        positionY: 'top'
    });
}

// 「选中了哪些行」按钮的 click 事件处理函数（标签上写 ClickHandler="onNotifySelectedRowsClick"）。
//
// 前提就写死在这一行里：本页有且只有一个表格，它的 id 就是 Grid1。示例页面都满足这个前提，所以能共用
// 这一个函数；自己的页面不满足时别改这个共享文件，在页面脚本区包一层，把表格的客户端 id 传给
// 下面的 notifySelectedRows。
//
// FineUI.Pro 还多一条前提：它的客户端 id 是渲染后的 ClientID，而每个控件都是命名容器——表格只要嵌在
// 别的控件里（面板 / 窗体 / 表单 / 选项卡），或者页面用了母版页、用户控件，id 就不再是 Grid1，
// 这个函数直接不适用。此时在页面里包一层，把渲染后的 id 传进来。
function onNotifySelectedRowsClick(event) {
    notifySelectedRows('Grid1');
}

// 公共方法 - 通过消息框展示表格选中的行
// 参数 gridId 是表格的客户端 id：各端页面上写什么 id 就是什么；FineUI.Pro 在命名容器里要传 ClientID。
function notifySelectedRows(gridId) {
    var grid = F(gridId);

    if (!grid.hasSelection()) {
        F.alert(_R('NoSelectionMessage'));
        return;
    }

    var genderColumn = grid.getColumn('Gender') || grid.getColumn('gender');
    var majorColumn = grid.getColumn('Major') || grid.getColumn('major');

    var table = $('<table>').addClass('result');
    var tr = $('<tr>').appendTo(table);
    if (grid.idField) {
        tr.append('<th>' + _R('GridRowId') + '</th>');
    }
    if (grid.textField) {
        tr.append('<th>' + _R('GridRowText') + '</th>');
    }
    if (genderColumn) {
        tr.append('<th>' + _R('GridGender') + '</th>');
    }
    if (majorColumn) {
        tr.append('<th>' + _R('GridMajor') + '</th>');
    }

    $.each(grid.getSelectedRows(true), function (index, row) {
        tr = $('<tr>').appendTo(table);
        if (grid.idField) {
            tr.append($('<td>').text(row.id));
        }
        if (grid.textField) {
            tr.append($('<td>').text(row.text));
        }
        if (genderColumn) {
            // 单元格值键 = 列 columnId（Java 数据注解版为小写字段名，Core/Pro 版为 PascalCase 字段名）
            var genderValue = row.values[genderColumn.columnId] !== undefined ? row.values[genderColumn.columnId] : row.values['Gender'];
            // 兼容单元格值为HTML片段的情况（FineUI.Pro） 
            if (F.product == 'FineUI.Pro') {
                genderValue = $(genderValue).text();
            } else {
                genderValue = genderValue == 1 ? _R('Male') : _R('Female');
            }
            tr.append($('<td>').text(genderValue));
        }
        if (majorColumn) {
            var majorValue = row.values[majorColumn.columnId] !== undefined ? row.values[majorColumn.columnId] : row.values['Major'];
            // 兼容单元格值为HTML片段的情况（FineUI.Pro） 
            if (F.product == 'FineUI.Pro') {
                majorValue = $(majorValue).text();
            }
            tr.append($('<td>').text(majorValue));
        }
    });

    showNotify(F.rawHtml(table[0].outerHTML));
}
