/**
 * 学生表格示例的共用显示与选择辅助函数，包括性别、专业链接、分组图标、徽章、头像、评分、行扩展和选择结果通知。
 * 由各表格示例按需引入 res/js/grid.js，依赖 FineUI 和 jQuery；加载时同时登记本文件使用的中英文资源键。
 * 列通过 RendererFunction 或 renderer 引用对应函数，选择按钮通过点击回调调用 notifySelectedRows(gridId)。
 * 渲染函数要求页面提供相应的数据字段；表格 ID 作为参数传入，Pro 使用服务器输出的 ClientID。
 */

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

/**
 * 把学生状态 1、2、3、4 渲染为优秀、良好、补考、重修徽章，并为部分状态增加动画。
 * 作为状态列的 RendererFunction 或 renderer；value 为状态值，返回可直接显示的 jQuery 元素。
 */
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

/**
 * 把头像、姓名、性别和入学年份组合为一块用户资料。
 * 作为资料列的渲染函数；params.rowData.attrs 须提供 Avatar、Name、Gender、EntranceYear，value 不参与显示。
 */
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

/**
 * 以只读点赞图标显示评分，用户不能在该显示区域改变分数。
 * 作为评分列渲染函数传入数值 value，返回 F.rateHtml 生成的内容。
 */
function renderRate1(value) {
    return F.rateHtml(value, {
        readonly: true,
        iconFont: 'f-iconfont-thumbs-up'
    });
}



/**
 * 把 1 或男转换为当前语言的男性文字，其余值显示女性文字。
 * 可作为性别列渲染函数，也可由其他渲染函数调用 renderGender(value)。
 */
function renderGender(value, params) {
    return (value == 1 || value === '男') ? _R('Male') : _R('Female');
}

/**
 * 把非空专业名称显示为新窗口打开的搜索链接，并提供完整文字提示。
 * 作为专业列渲染函数；名称分别按 HTML 和 URL 规则编码，空字符串返回空内容。
 */
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

/**
 * 按分组值显示 res/images/16 下的对应图标，空字符串不显示图片。
 * 作为分组列渲染函数；例如值 1 对应 1.png，返回图片的 jQuery 元素。
 */
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

/**
 * 生成一行的姓名和简介详情，放在该行的展开区域。
 * 绑定到行扩展列；value 是简介，params.rowData.values.Name 是姓名，本例按 HTML 组合演示内容。
 */
function renderExpander(value, params) {
    var rowData = params.rowData;
    return $('<div>').addClass('expander')
        .append($('<p>').html('<strong>' + _R('NamePrefix') + '</strong>' + rowData.values['Name']))
        .append($('<p>').html('<strong>' + _R('IntroPrefix') + '</strong>' + value));
}

/**
 * 生成带删除图标的行内链接，供页面自己的点击处理识别。
 * 作为操作列渲染函数使用；本函数只生成入口，点击后的确认、回发或删除由页面另行处理。
 */
function renderDeleteAction(value, params) {
    return $('<a>').addClass('action-btn delete').attr('href', '#')
        .append($('<img>').addClass('f-grid-cell-icon').attr('src', F.resolveUrl('/res/icon/delete.png')).attr('alt', 'Delete'));
}


/**
 * 在顶层页面上方显示选择结果等长消息，不额外占用图标空间。
 * 由本文件的选择结果函数调用，也可调用 showNotify(content)；要显示已生成的表格 HTML 时传入 F.rawHtml(html)。
 */
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

/**
 * 为客户端 ID 恰好是 Grid1 的单表格页面显示当前选择。
 * 按钮可声明 ClickHandler="onNotifySelectedRowsClick"；其他 ID 请在页面回调中调用 notifySelectedRows(actualClientId)。
 * Pro 的嵌套控件应使用服务器输出的 ClientID，不拼接面板或表单前缀。
 */
function onNotifySelectedRowsClick(event) {
    notifySelectedRows('Grid1');
}

/**
 * 读取表格当前选中行，用通知表格显示主键、文字及已存在的性别、专业列；未选择时给出提示。
 * 调用 notifySelectedRows(gridId, additionalColumns)；gridId 为实际客户端 ID，additionalColumns 可省略。
 * 附加列示例：[{ columnId: 'AtSchool', text: '是否在校' }]；读取当前行数据，不向后台提交或保存。
 */
function notifySelectedRows(gridId, additionalColumns) {
    var grid = F(gridId);
    additionalColumns = additionalColumns || [];

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
    additionalColumns.forEach(function (column) {
        tr.append($('<th>').text(column.text));
    });

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
            // 专属示例仍可能保存旧模板的 HTML；普通渲染列保存 0/1，直接转换成男/女。
            if (typeof genderValue === 'string' && /^\s*</.test(genderValue)) {
                genderValue = $(genderValue).text();
            } else {
                genderValue = renderGender(genderValue);
            }
            tr.append($('<td>').text(genderValue));
        }
        if (majorColumn) {
            var majorValue = row.values[majorColumn.columnId] !== undefined ? row.values[majorColumn.columnId] : row.values['Major'];
            // 仅解析旧列生成的 HTML，专业名称本身不能当作 jQuery 选择器。
            if (typeof majorValue === 'string' && /^\s*</.test(majorValue)) {
                majorValue = $(majorValue).text();
            }
            tr.append($('<td>').text(majorValue));
        }
        // 复选框示例可以附加自己的列；读取当前数据，勾选与取消后的结果都及时显示。
        additionalColumns.forEach(function (column) {
            var value = row.values[column.columnId];
            tr.append($('<td>').text(value == null ? '' : String(value)));
        });
    });

    showNotify(F.rawHtml(table[0].outerHTML));
}
