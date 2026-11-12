(function () {
    'use strict';

    // 源码页只展示示例的实现；例如去掉 @section exampleDescription 整块，保留前后的控件和脚本。
    // 直接按原始文本删除对应区间，不用 DOM 重新序列化 Razor 或模板，避免改变标签大小写及转义。
    function filter(source) {
        var ranges = [];
        var tags = [];
        // 把注释和 script/style 整段跳过：例如 JS 字符串中的 <template> 不是页面声明，属性里的 > 也不结束标签。
        var tokenPattern = /<!--[\s\S]*?-->|<script\b(?:[^"'<>]|"[^"]*"|'[^']*')*>[\s\S]*?<\/script\s*>|<style\b(?:[^"'<>]|"[^"]*"|'[^']*')*>[\s\S]*?<\/style\s*>|<\/?(section|template|th:block)\b(?:[^"'<>]|"[^"]*"|'[^']*')*>/gi;
        var match;
        while ((match = tokenPattern.exec(source))) {
            if (match[1]) {
                tags.push({ start: match.index, end: tokenPattern.lastIndex, name: match[1].toLowerCase(), text: match[0] });
            }
        }

        function closingTag(index) {
            var opening = tags[index];
            var depth = 1;
            for (var next = index + 1; next < tags.length; next++) {
                var tag = tags[next];
                if (tag.name !== opening.name || /\/\s*>$/.test(tag.text)) {
                    continue;
                }
                depth += /^<\//.test(tag.text) ? -1 : 1;
                if (depth === 0) {
                    return tag.end;
                }
            }
            return -1;
        }

        function addRange(start, end) {
            // 描述独占一组行时连同缩进和行尾一起删除，避免源码留下大片空白或把相邻行粘到一起。
            var lineStart = source.lastIndexOf('\n', start - 1) + 1;
            if (!source.slice(lineStart, start).trim()) {
                start = lineStart;
            }
            var lineEnd = source.indexOf('\n', end);
            if (lineEnd >= 0 && !source.slice(end, lineEnd).trim()) {
                end = lineEnd + 1;
            }
            ranges.push({ start: start, end: end });
        }

        for (var index = 0; index < tags.length; index++) {
            var tag = tags[index];
            var template = tag.name === 'template' && /\sid\s*=\s*(["'])example-description\1/i.test(tag.text);
            var fragment = tag.name === 'th:block' && /\slayout:fragment\s*=\s*(["'])exampleDescription\1/.test(tag.text);
            if ((template || fragment) && !/^<\//.test(tag.text)) {
                var end = closingTag(index);
                if (end >= 0) {
                    addRange(tag.start, end);
                }
            }
        }

        var sectionPattern = /^[ \t]*@section\s+exampleDescription\s*\{\s*(?=<section\b)/gm;
        while ((match = sectionPattern.exec(source))) {
            var sectionIndex = tags.findIndex(function (tag) {
                return tag.start === sectionPattern.lastIndex && tag.name === 'section';
            });
            if (sectionIndex < 0) {
                continue;
            }
            var sectionEnd = closingTag(sectionIndex);
            if (sectionEnd < 0) {
                continue;
            }
            var brace = /^[ \t\r\n]*\}/.exec(source.slice(sectionEnd));
            if (brace) {
                addRange(match.index, sectionEnd + brace[0].length);
            }
        }

        ranges.sort(function (left, right) {
            return left.start - right.start || right.end - left.end;
        });
        var result = '';
        var position = 0;
        ranges.forEach(function (range) {
            if (range.start >= position) {
                result += source.slice(position, range.start);
                position = range.end;
            }
        });
        return result + source.slice(position);
    }

    function filterDocument() {
        // 后台类、JS 和 CSS 的源码没有这个标记，即使正文提到描述模板，也不会被当成页面声明删除。
        document.querySelectorAll('pre[data-example-page-source]').forEach(function (element) {
            element.textContent = filter(element.textContent);
        });
    }

    window.FineUISourceCodeFilter = { filter: filter, filterDocument: filterDocument };
}());
