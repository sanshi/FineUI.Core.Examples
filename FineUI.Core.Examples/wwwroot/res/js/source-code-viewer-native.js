/**
 * 为不加载 FineUI 的原生 HTML 对照页提供源码入口，避免为了一个按钮就给演示页引入整套控件运行时。
 * 原生页通过 meta[name="fineui-native-source-window"] 指定同源查看器辅助页，并在 body 准备好后引入本文件及查看器 CSS。
 * 脚本只在独立顶层页面创建右下角入口；点击后加载辅助 iframe，由辅助页内的 source-code-viewer.js 显示 F.Window。
 * 辅助页关闭窗口时调用父页 FineUISourceViewerNative.close()，移除 iframe 并把焦点还给入口按钮。
 */

(function () {
    'use strict';

    var address = document.querySelector('meta[name="fineui-native-source-window"]');
    if (!address || window !== window.top) {
        return;
    }

    var frame;
    var viewerScriptUrl = document.currentScript.src;
    var launcher = document.createElement('button');
    launcher.id = 'exampleDetailsLauncher';
    launcher.type = 'button';
    launcher.className = 'source-code-viewer-launcher';
    launcher.title = '查看页面描述和源代码';
    launcher.setAttribute('aria-label', launcher.title);

    // 原生 HTML 示例不引入字体图标资源；用相同含义的代码符号提供查看器入口。
    var icon = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
    icon.setAttribute('viewBox', '0 0 24 24');
    icon.setAttribute('width', '24');
    icon.setAttribute('height', '24');
    icon.setAttribute('aria-hidden', 'true');
    var path = document.createElementNS('http://www.w3.org/2000/svg', 'path');
    path.setAttribute('d', 'M8 5 2 12l6 7M16 5l6 7-6 7M14 3l-4 18');
    path.setAttribute('fill', 'none');
    path.setAttribute('stroke', 'currentColor');
    path.setAttribute('stroke-width', '1.5');
    icon.appendChild(path);
    launcher.appendChild(icon);
    launcher.addEventListener('click', open);
    document.body.appendChild(launcher);
    var positionScript = document.createElement('script');
    positionScript.src = new URL('source-code-launcher.js', viewerScriptUrl).href;
    document.head.appendChild(positionScript);

    /**
     * 加载原生页面配置的查看器辅助 iframe，并临时隐藏入口，避免重复打开。
     * 仅作为角落入口按钮的 click 回调使用；辅助页地址来自 fineui-native-source-window 元信息。
     */
    function open() {
        if (frame) {
            return;
        }
        frame = document.createElement('iframe');
        frame.id = 'exampleNativeSourceWindow';
        frame.className = 'source-code-viewer-native-frame';
        frame.title = '页面描述和源代码';
        frame.src = address.content;
        launcher.hidden = true;
        document.body.appendChild(frame);
        frame.focus();
    }

    /**
     * 移除查看器辅助 iframe，重新显示入口并恢复焦点。
     * 对外通过 FineUISourceViewerNative.close() 调用，通常由辅助页窗口的关闭流程调用；未打开时可重复调用。
     */
    function close() {
        if (!frame) {
            return;
        }
        frame.remove();
        frame = null;
        launcher.hidden = false;
        launcher.focus();
    }

    window.FineUISourceViewerNative = { close: close };
}());
