/**
 * 全局配置查询页的交互：按分类和关键词筛选配置，展开详情，并生成当前产品对应的配置写法。
 * 由 Config/GlobalProperties 等配置清单页面引入，依赖 FineUI、jQuery 和 configPropertyCatalog 元素。
 * 该元素通过 data-grid、data-category、data-search、data-count 指定实际控件 ID，data-format 指定配置语法；清单来自自身 JSON 或 data-url。
 * 引入后由 F.ready 自动初始化一次；页面将 onConfig 开头的函数绑定到控件事件，将 ConfigPropertyQuery 的渲染函数绑定到表格列。
 */

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

    /**
     * 把配置初始值转换为可阅读的文字，区分空字符串与没有文字的显示效果。
     * 初始化清单时调用 formatInitialValue(value)；例如空字符串显示为双引号并注明空字符串，布尔值显示为 true 或 false。
     */
    function formatInitialValue(value) {
        if (value === '') {
            return '""（空字符串）';
        }
        if (typeof value === 'string') {
            return value;
        }
        return JSON.stringify(value);
    }

    /**
     * 为一项配置生成可复制的写法，处理 properties、XML、JavaScript 或 JSON 的语法差异。
     * 由 renderDetail 生成详情时调用；row.example 提供示例值，页面 data-format 决定输出格式。
     */
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

    /**
     * 生成配置名称单元格，已废弃的配置加删除线，名称按文本插入。
     * 作为列渲染函数使用 ConfigPropertyQuery.renderName；name 是清单中唯一的配置名。
     */
    function renderName(name) {
        const row = propertiesByName.get(name);
        return row.obsolete ? $('<s>').text(name) : $('<span>').text(name);
    }

    /**
     * 生成配置的完整说明、类型、初始值、可选项和写法，废弃项改为显示迁移说明。
     * 绑定到行扩展列的 RendererFunction 或 renderer；通过 params.rowId 查找配置，返回 jQuery 元素。
     */
    function renderDetail(value, params) {
        // 行 ID 始终是配置名。WebForms 为扩展列单独生成字段名，传入的单元格值可能为空。
        const row = propertiesByName.get(params.rowId);
        const detail = $('<div>').addClass('config-property-detail');
        detail.append($('<h3>').append(renderName(row.name)));

        /**
         * 向当前配置详情追加一项文字说明，标签和内容均按文本处理。
         * 仅供 renderDetail 内部调用，例如 addLine('类型', row.valueType)。
         */
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

    /**
     * 把当前分类、关键词和展开项写回地址，复制该地址可恢复同一查询结果。
     * 由筛选和展开处理调用；使用 replaceState 更新 category、q、property，不新增历史记录。
     */
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

    /**
     * 按当前筛选条件重载配置表格，并在需要时翻到目标配置所在页、展开详情。
     * 内部调用 render(requestedName)；传配置名用于恢复分享地址，传空字符串表示只刷新筛选结果。
     */
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

    /**
     * 合并连续输入引起的搜索，只在最后一次变化后等待 120 毫秒再查询。
     * 页面的 change 回调通过 ConfigPropertyQuery.queueSearch() 调用，避免每个按键都重载表格。
     */
    function queueSearch() {
        window.clearTimeout(searchTimer);
        searchTimer = window.setTimeout(() => render(''), 120);
    }

    /**
     * 从地址恢复分类、关键词及展开项，保证通过历史记录返回时也能看到对应配置。
     * 由初始化和 popstate 调用；取消设置筛选控件时排队的搜索，防止目标详情被再次收起。
     */
    function readAddress() {
        const params = new URLSearchParams(window.location.search);
        const category = params.get('category');
        categoryField.setValue([...propertiesByName.values()].some(row => row.category === category) ? category : '');
        searchField.setValue(params.get('q') || '');

        // 恢复筛选控件也会触发 change，取消排队的搜索才能保留地址指定的详情。
        window.clearTimeout(searchTimer);
        render(params.get('property'));
    }

    /**
     * 读取配置清单并连接页面上的筛选框、表格和数量提示，然后恢复地址中的查询条件。
     * 本文件已用 F.ready(ConfigPropertyQuery.initialize) 登记调用；页面应在引入前准备 configPropertyCatalog，无需再手工调用。
     */
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

    /**
     * 记录当前展开的配置名，使分享地址与实际详情状态一致。
     * 由行展开和收起回调调用，例如 ConfigPropertyQuery.recordExpansion(rowId, true) 表示该配置已展开。
     */
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

/**
 * 分类或关键词改变时安排一次延迟筛选。
 * 将本函数绑定到分类下拉框、搜索框的 change 事件。
 */
function onConfigQueryChange() {
    ConfigPropertyQuery.queueSearch();
}

/**
 * 点击配置行时切换该行的详情展开状态。
 * 绑定到表格 rowclick；this 是当前表格，rowId 是作为行标识的配置名。
 */
function onConfigRowClick(event, rowId) {
    this.toggleRowExpanders([rowId]);
}

/**
 * 详情展开后把配置名记入地址。
 * 绑定到表格 rowexpanderexpand；框架传入 event 和 rowId。
 */
function onConfigRowExpand(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, true);
}

/**
 * 详情收起后清除该配置对应的地址参数。
 * 绑定到表格 rowexpandercollapse；只有收起项恰好是已记录的配置时才清除。
 */
function onConfigRowCollapse(event, rowId) {
    ConfigPropertyQuery.recordExpansion(rowId, false);
}

F.ready(ConfigPropertyQuery.initialize);
