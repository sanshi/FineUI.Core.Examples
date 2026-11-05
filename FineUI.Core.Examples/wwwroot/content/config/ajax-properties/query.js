// 查询只读取随示例发布的属性清单；筛选、分页和展开均由浏览器完成。
window.AjaxPropertyQuery = (function () {
    'use strict';

    const names = {
        Button: '按钮', Grid: '表格', TextBox: '文本框', TextArea: '多行文本框',
        NumberBox: '数字输入框', DatePicker: '日期选择器', Calendar: '日历',
        TimePicker: '时间选择器', DropDownList: '下拉列表', DropDownBox: '下拉框',
        CheckBox: '复选框', CheckBoxList: '复选列表', RadioButton: '单选框',
        RadioButtonList: '单选列表', Panel: '面板', ContentPanel: '内容面板',
        Form: '表单', SimpleForm: '简单表单', FormRow: '表单行', DataList: '数据列表',
        Tree: '树', Window: '窗口', TabStrip: '选项卡', Tab: '选项卡页',
        Toolbar: '工具栏', PagingToolbar: '分页工具栏', Label: '标签', Image: '图片',
        HyperLink: '链接', LinkButton: '链接按钮', FileUpload: '文件上传',
        TriggerBox: '触发输入框', TwinTriggerBox: '双触发输入框', HiddenField: '隐藏字段',
        Accordion: '折叠面板组', AccordionPane: '折叠面板', GroupPanel: '分组面板',
        RegionPanel: '区域布局面板', Region: '区域', HtmlEditor: '富文本编辑器',
        Menu: '菜单', MenuButton: '菜单按钮', MenuCheckBox: '菜单复选项',
        MenuHyperLink: '菜单链接', MenuText: '菜单文字', MenuSeparator: '菜单分隔符',
        ButtonGroup: '按钮组', Tool: '工具按钮', ToolbarFill: '工具栏填充',
        ToolbarSeparator: '工具栏分隔符', ToolbarText: '工具栏文字',
    };
    let catalog;
    let propertiesById = new Map();
    let selectedProperty = '';
    let searchTimer;

    function controlType() {
        return F.ui.ddlControlType.getValue() || 'all';
    }

    function updateAddress() {
        const url = new URL(window.location.href);
        url.searchParams.set('control', controlType());
        if (F.ui.cbxExcludeInherited.getValue()) {
            url.searchParams.set('excludeInherited', 'true');
        } else {
            url.searchParams.delete('excludeInherited');
        }
        const keyword = F.ui.tbxPropertySearch.getValue().trim();
        if (keyword) {
            url.searchParams.set('q', keyword);
        } else {
            url.searchParams.delete('q');
        }
        if (selectedProperty) {
            url.searchParams.set('property', selectedProperty);
        } else {
            url.searchParams.delete('property');
        }
        // 保留页面所在的部署子目录及其它查询参数，刷新后仍可恢复筛选条件。
        window.history.replaceState(null, '', url);
    }

    function renderDetail(id) {
        const row = propertiesById.get(id);
        if (!row) {
            return '';
        }
        // 说明来自普通文本，使用 text 写入，避免属性描述中的尖括号被当作 HTML。
        const detail = $('<div>').addClass('ajax-property-detail');
        detail.append($('<h3>').text(row.control + '.' + row.property));
        for (const [label, value] of [
            ['含义', row.description],
            ['生效时机', catalog.effects[row.effect]],
            ['使用条件', row.note],
            ['声明来源', row.owner + (row.alias ? '（' + row.alias + '）' : '')],
            ['属性类型', row.type],
        ]) {
            detail.append($('<p>').append($('<strong>').text(label + '：'), document.createTextNode(value)));
        }
        detail.append($('<p>').append($('<strong>').text('回发事件中的赋值方式：')));
        detail.append($('<pre>').append($('<code>').text(row.control + '1.' + row.property + ' = newValue;')));
        return detail;
    }

    function render(preserveSelection) {
        if (!catalog) {
            return;
        }
        if (!preserveSelection) {
            selectedProperty = '';
        }
        const type = controlType();
        const excludeInherited = F.ui.cbxExcludeInherited.getValue();
        const words = F.ui.tbxPropertySearch.getValue().toLowerCase().trim().split(/\s+/).filter(Boolean);
        const rows = catalog.properties.filter(row => {
            if (type !== 'all' && row.control !== type) {
                return false;
            }
            // ContentPanel 在 Core 中是 Panel 的标签别名，仍按 Panel 的声明来源判断。
            const declaredType = row.control === 'ContentPanel' ? 'Panel' : row.control;
            if (excludeInherited && row.owner !== declaredType) {
                return false;
            }
            const text = [
                row.control, names[row.control], row.property, row.method, row.description, row.note,
            ].join(' ').toLowerCase();
            return words.every(word => text.includes(word));
        });
        const selectedIndex = rows.findIndex(row => row.id === selectedProperty);
        if (selectedIndex < 0) {
            selectedProperty = '';
        }
        const grid = F.ui.Grid1;
        grid.setPageIndex(0);
        grid.loadData(rows);
        F.ui.labResultCount.setText('找到 ' + rows.length + ' 项 AJAX 属性');

        // 地址指定的属性可能在第二页以后，先翻到对应页，再展开该行。
        if (selectedIndex >= 0) {
            grid.loadPageData(Math.floor(selectedIndex / grid.getPageSize()));
            grid.expandRowExpanders([selectedProperty]);
        }
        updateAddress();
    }

    function search() {
        window.clearTimeout(searchTimer);
        render(false);
    }

    function queueSearch() {
        window.clearTimeout(searchTimer);
        searchTimer = window.setTimeout(search, 120);
    }

    function onRowClick(event, rowId) {
        F.ui.Grid1.toggleRowExpanders([rowId]);
    }

    function onExpand(event, rowId) {
        selectedProperty = rowId;
        updateAddress();
    }

    function onCollapse(event, rowId) {
        if (selectedProperty === rowId) {
            selectedProperty = '';
            updateAddress();
        }
    }

    function readAddress() {
        if (!catalog) {
            return;
        }
        const parameters = new URLSearchParams(window.location.search);
        const requestedType = parameters.get('control') || 'Button';
        const type = catalog.properties.find(row => row.control.toLowerCase() === requestedType.toLowerCase())?.control;
        F.ui.ddlControlType.setValue(type || '');
        F.ui.tbxPropertySearch.setValue(parameters.get('q') || '');
        F.ui.cbxExcludeInherited.setValue(parameters.get('excludeInherited') === 'true');
        // 恢复地址条件也会触发输入控件的 change，取消这一轮排队，保留即将展开的详情。
        window.clearTimeout(searchTimer);
        selectedProperty = parameters.get('property') || '';
        render(true);
    }

    async function loadCatalog() {
        F.ui.btnRetryCatalog.hide();
        F.ui.labResultCount.setText('正在读取属性清单…');
        try {
            const url = document.getElementById('ajaxPropertyCatalog').dataset.catalogUrl;
            const response = await fetch(url, { credentials: 'same-origin' });
            if (!response.ok) {
                throw new Error('属性清单请求失败');
            }
            const data = await response.json();
            if (!Array.isArray(data.properties) || !data.effects) {
                throw new Error('属性清单格式错误');
            }
            catalog = data;
            catalog.properties = data.properties.map(row => ({
                ...row,
                id: row.control + '.' + row.property,
                effectText: data.effects[row.effect],
            }));
            propertiesById = new Map(catalog.properties.map(row => [row.id, row]));
            const types = [...new Set(catalog.properties.map(row => row.control))];
            F.ui.ddlControlType.loadData(types.map(type => ({
                value: type,
                text: names[type] ? type + ' · ' + names[type] : type,
                enabled: true,
            })));
            readAddress();
        } catch (error) {
            F.ui.labResultCount.setText('属性清单暂时无法读取，请重试。');
            F.ui.btnRetryCatalog.show();
        }
    }

    window.addEventListener('popstate', readAddress);
    return { loadCatalog, queueSearch, renderDetail, onRowClick, onExpand, onCollapse };
})();

// 声明事件使用固定的全局函数入口，供筛选、重试和行展开调用。
function onAjaxPropertyQueryChange() {
    return AjaxPropertyQuery.queueSearch();
}

function onAjaxPropertyQueryRetry() {
    return AjaxPropertyQuery.loadCatalog();
}

function onAjaxPropertyQueryRowClick(event, rowId) {
    return AjaxPropertyQuery.onRowClick(event, rowId);
}

function onAjaxPropertyQueryExpand(event, rowId) {
    return AjaxPropertyQuery.onExpand(event, rowId);
}

function onAjaxPropertyQueryCollapse(event, rowId) {
    return AjaxPropertyQuery.onCollapse(event, rowId);
}

F.ready(AjaxPropertyQuery.loadCatalog);
