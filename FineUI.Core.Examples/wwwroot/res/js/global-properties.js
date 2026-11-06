window.ConfigPropertyQuery = (function () {
    'use strict';

    let grid;
    let categoryField;
    let searchField;
    let countLabel;
    let exampleFormat;
    const propertiesByName = new Map();
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

    function formatExample(row) {
        if (exampleFormat === 'properties') {
            const value = Array.isArray(row.example) ? row.example.join(',') : String(row.example);
            // properties 会忽略等号后的前导空格，反斜杠也是转义符。例如一个空格须写成 "\\ "。
            const escaped = value.replace(/\\/g, '\\\\').replace(/^ +/, spaces => '\\ '.repeat(spaces.length));
            return row.name + '=' + escaped;
        }
        if (exampleFormat === 'xml') {
            const value = Array.isArray(row.example) ? row.example.join(',') : String(row.example);
            const escaped = value.replace(/&/g, '&amp;').replace(/"/g, '&quot;')
                .replace(/</g, '&lt;').replace(/>/g, '&gt;');
            return row.name + '="' + escaped + '"';
        }
        const key = exampleFormat === 'javascript' ? row.name : JSON.stringify(row.name);
        return key + ': ' + JSON.stringify(row.example, null, 2);
    }

    function renderName(name) {
        const row = propertiesByName.get(name);
        return row.obsolete ? $('<s>').text(name) : $('<span>').text(name);
    }

    function renderDetail(value, params) {
        // 行 ID 始终是配置名。WebForms 为扩展列单独生成字段名，传入的单元格值可能为空。
        const row = propertiesByName.get(params.rowId);
        const detail = $('<div>').addClass('config-property-detail');
        detail.append($('<h3>').append(renderName(row.name)));

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
            const label = document.getElementById('configPropertyCatalog').dataset.exampleLabel;
            detail.append($('<p>').append($('<strong>').text(label)));
            detail.append($('<pre>').append($('<code>').text(formatExample(row))));
        }
        return detail;
    }

    function updateAddress() {
        const url = new URL(window.location.href);
        const filters = {
            category: categoryField.getValue(),
            q: searchField.getValue().trim(),
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
        const category = categoryField.getValue();
        const words = searchField.getValue().trim().toLowerCase().split(/\s+/).filter(Boolean);
        const rows = [...propertiesByName.values()].filter(row => {
            if (category && row.category !== category) {
                return false;
            }
            return words.every(word => row.searchText.includes(word));
        });

        grid.setPageIndex(0);
        grid.loadData(rows);
        countLabel.setText('找到 ' + rows.length + ' 项全局配置');
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
        categoryField.setValue([...propertiesByName.values()].some(row => row.category === category) ? category : '');
        searchField.setValue(params.get('q') || '');

        // 恢复筛选控件也会触发 change，取消排队的搜索才能保留地址指定的详情。
        window.clearTimeout(searchTimer);
        render(params.get('property'));
    }

    async function initialize() {
        const element = document.getElementById('configPropertyCatalog');
        const config = element.dataset;
        grid = F(config.grid);
        categoryField = F(config.category);
        searchField = F(config.search);
        countLabel = F(config.count);
        exampleFormat = config.format;

        // 服务端可直接嵌入空配置的清单；静态 HTML 从随示例发布的 JSON 读取。
        let catalog;
        if (config.url) {
            try {
                const response = await fetch(config.url);
                if (!response.ok) {
                    throw new Error('无法读取全局配置清单');
                }
                catalog = await response.json();
            } catch {
                countLabel.setText('配置清单读取失败，请刷新页面重试。');
                return;
            }
        } else {
            catalog = JSON.parse(element.textContent);
        }
        const categories = new Set();
        for (const row of catalog) {
            const initialText = row.initialText || formatInitialValue(row.initialValue);
            const searchText = [row.name, row.category, row.description, row.valueType,
                initialText, row.options, row.note].join(' ').toLowerCase();
            propertiesByName.set(row.name, { ...row, initialText, searchText });
            categories.add(row.category);
        }

        categoryField.loadData([...categories].map(category => ({ value: category, text: category, enabled: true })));
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

    return { initialize, queueSearch, renderName, renderDetail, recordExpansion };
})();

function onConfigQueryChange() {
    ConfigPropertyQuery.queueSearch();
}

function onConfigRowClick(event, rowId) {
    this.toggleRowExpanders([rowId]);
}

function onConfigRowExpand(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, true);
}

function onConfigRowCollapse(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, false);
}

F.ready(ConfigPropertyQuery.initialize);
