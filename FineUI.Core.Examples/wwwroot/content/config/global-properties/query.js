window.ConfigPropertyQuery = (function () {
    'use strict';

    let properties = [];
    let expandedName = '';
    let searchTimer;

    function formatInitialValue(value) {
        if (value === '') {
            return '""（空字符串）';
        }
        if (typeof value === 'string') {
            return value;
        }
        return JSON.stringify(value);
    }

    function renderDetail(name) {
        const row = properties.find(property => property.name === name);
        const detail = $('<div>').addClass('config-property-detail');
        detail.append($('<h3>').text(row.name));

        function addLine(label, value) {
            detail.append($('<p>').append($('<strong>').text(label + '：'), document.createTextNode(value)));
        }

        addLine('分类', row.category);
        addLine('含义', row.description);
        addLine('类型', row.valueType);
        addLine('初始值', row.initialText);
        addLine('可选值或格式', row.options);
        if (row.note) {
            addLine(row.obsolete ? '迁移说明' : '使用说明', row.note);
        }
        if (!row.obsolete) {
            // 只展示 FineUI 节内的键值对，例如 "AjaxTimeout": 60。数组保留换行，字符串按 JSON 规则转义。
            const entry = JSON.stringify(row.name) + ': ' + JSON.stringify(row.example, null, 2);
            detail.append($('<p>').append($('<strong>').text('FineUI 节内写法')));
            detail.append($('<pre>').append($('<code>').text(entry)));
        }
        return detail;
    }

    function updateAddress() {
        const url = new URL(window.location.href);
        const filters = {
            category: F.ui.ddlCategory.getValue(),
            q: F.ui.tbxSearch.getValue().trim(),
            property: expandedName,
        };
        for (const [key, value] of Object.entries(filters)) {
            if (value) {
                url.searchParams.set(key, value);
            } else {
                url.searchParams.delete(key);
            }
        }
        window.history.replaceState(null, '', url);
    }

    function render(requestedName) {
        const category = F.ui.ddlCategory.getValue();
        const words = F.ui.tbxSearch.getValue().trim().toLowerCase().split(/\s+/).filter(Boolean);
        const rows = properties.filter(row => {
            if (category && row.category !== category) {
                return false;
            }
            return words.every(word => row.searchText.includes(word));
        });

        const grid = F.ui.Grid1;
        grid.setPageIndex(0);
        grid.loadData(rows);
        F.ui.labResultCount.setText('找到 ' + rows.length + ' 项全局配置');
        expandedName = '';

        // 从分享地址进入时，目标配置可能在第二页以后，先翻到对应页再展开。
        const index = rows.findIndex(row => row.name === requestedName);
        if (index >= 0) {
            expandedName = requestedName;
            grid.loadPageData(Math.floor(index / grid.getPageSize()));
            grid.expandRowExpanders([requestedName]);
        }
        updateAddress();
    }

    function queueSearch() {
        window.clearTimeout(searchTimer);
        searchTimer = window.setTimeout(() => render(''), 120);
    }

    function readAddress() {
        const params = new URLSearchParams(window.location.search);
        const category = params.get('category');
        F.ui.ddlCategory.setValue(properties.some(row => row.category === category) ? category : '');
        F.ui.tbxSearch.setValue(params.get('q') || '');

        // 恢复筛选控件也会触发 change，取消排队的搜索才能保留地址指定的详情。
        window.clearTimeout(searchTimer);
        render(params.get('property'));
    }

    function initialize() {
        const catalog = JSON.parse(document.getElementById('configPropertyCatalog').textContent);
        properties = catalog.map(row => {
            const initialText = formatInitialValue(row.initialValue);
            const searchText = [row.name, row.category, row.description, row.valueType,
                initialText, row.options, row.note].join(' ').toLowerCase();
            return { ...row, initialText, searchText };
        });

        const categories = [...new Set(properties.map(row => row.category))];
        F.ui.ddlCategory.loadData(categories.map(category => ({ value: category, text: category, enabled: true })));
        readAddress();
        window.addEventListener('popstate', readAddress);
    }

    function recordExpansion(name, expanded) {
        if (expanded) {
            expandedName = name;
        } else if (expandedName === name) {
            expandedName = '';
        }
        updateAddress();
    }

    return { initialize, queueSearch, renderDetail, recordExpansion };
})();

function onConfigQueryChange() {
    ConfigPropertyQuery.queueSearch();
}

function onConfigRowClick(event, rowId) {
    F.ui.Grid1.toggleRowExpanders([rowId]);
}

function onConfigRowExpand(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, true);
}

function onConfigRowCollapse(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, false);
}

F.ready(ConfigPropertyQuery.initialize);
