(function () {
    'use strict';

    var address = document.querySelector('meta[name="fineui-native-source-window"]');
    if (!address || window !== window.top) {
        return;
    }

    var frame;
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
