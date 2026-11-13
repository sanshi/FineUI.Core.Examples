/**
 * 在显示页面源码时移除页面描述声明，使开发人员集中阅读示例控件和脚本。
 * 由各产品的单文件源码页在语法着色前引入，页面标记需要过滤的 pre[data-example-page-source]。
 * 调用 FineUISourceCodeFilter.filterDocument() 批量处理已标记源码；也可调用 filter(source) 取得过滤后的字符串。
 * 支持 Razor 的 exampleDescription 区段、HTML 描述模板及 Thymeleaf 描述片段；不重新序列化页面，也不负责 HTML 安全过滤。
 */

(function () {
    'use strict';

    /**
     * 从页面源码文本中删去描述声明，保留原控件、脚本和文本格式。
     * 调用 FineUISourceCodeFilter.filter(source)，source 为原始源码字符串，返回新字符串；脚本、样式及注释中的同名文字不当作页面声明删除。
     */
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

        /**
         * 为当前描述开标签找到匹配的结束位置，正确跨过同名嵌套标签。
         * 只在 filter 内部调用，index 指向已扫描的 tags 项；找不到完整闭合位置时返回 -1，保留原文。
         */
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

        /**
         * 把描述所在区间加入删除清单，并在描述独占一组行时连同缩进与行尾一起删除。
         * 只在 filter 内部调用，start、end 为原始字符串偏移；不使用 DOM 重写相邻源码。
         */
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

    /**
     * 只过滤标记为页面标签源码的 pre，避免误删后台类、JS、CSS 中用于讲解的文字。
     * 单文件源码页在语法着色前调用 FineUISourceCodeFilter.filterDocument()；未带 data-example-page-source 的内容保持原样。
     */
    function filterDocument() {
        // 后台类、JS 和 CSS 的源码没有这个标记，即使正文提到描述模板，也不会被当成页面声明删除。
        document.querySelectorAll('pre[data-example-page-source]').forEach(function (element) {
            element.textContent = filter(element.textContent);
        });
    }

    window.FineUISourceCodeFilter = { filter: filter, filterDocument: filterDocument };
}());
