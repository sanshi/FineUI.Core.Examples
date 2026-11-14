/**
 * 独立示例的源码入口自动避让当前可见的操作控件，避免遮住右下角登录按钮或移动工具栏。
 * 查看器创建入口后按需加载本文件；滚动、控件显隐和视口变化时重新寻找位置，不移动或操作示例控件。
 */
(function () {
    'use strict';

    var launcher = document.getElementById('exampleDetailsLauncher');
    if (!launcher) {
        return;
    }
    var scheduled = false;
    var gap = 8;
    var positionHint = document.createElement('span');
    positionHint.className = 'source-code-viewer-position-hint';
    positionHint.textContent = '拖动源码入口可让出操作位置';
    positionHint.hidden = true;
    launcher.after(positionHint);
    launcher.setAttribute('aria-keyshortcuts', 'Alt+Shift+S');
    var drag = null;
    var suppressClick = false;

    // 满屏没有空位时仍保留手机可点击的入口；移动入口只改变其位置，不转发或模拟演示控件的点击。
    launcher.addEventListener('pointerdown', function (event) {
        suppressClick = false;
        if (!launcher.classList.contains('source-code-viewer-launcher-movable')) {
            return;
        }
        var rect = launcher.getBoundingClientRect();
        drag = { id: event.pointerId, x: event.clientX, y: event.clientY, left: rect.left, top: rect.top, moved: false };
        launcher.setPointerCapture(event.pointerId);
    });
    launcher.addEventListener('pointermove', function (event) {
        if (!drag || event.pointerId !== drag.id) {
            return;
        }
        var dx = event.clientX - drag.x;
        var dy = event.clientY - drag.y;
        drag.moved = drag.moved || Math.abs(dx) + Math.abs(dy) > 6;
        if (drag.moved) {
            var left = Math.max(gap, Math.min(window.innerWidth - 44 - gap, drag.left + dx));
            var top = Math.max(gap, Math.min(window.innerHeight - 44 - gap, drag.top + dy));
            launcher.style.right = window.innerWidth - left - 44 + 'px';
            launcher.style.bottom = window.innerHeight - top - 44 + 'px';
        }
    });
    function finishDrag(event) {
        if (drag && event.pointerId === drag.id) {
            suppressClick = drag.moved;
            drag = null;
            // 部分触屏浏览器在指针捕获后不再生成兼容 click；轻触时仍执行入口自己的打开动作。
            // 先完成这次打开，再吞掉可能随后到达的兼容 click，避免一次轻触打开两次。
            if (event.pointerType === 'touch' && !suppressClick) {
                event.preventDefault();
                launcher.click();
                suppressClick = true;
            }
        }
    }
    launcher.addEventListener('pointerup', finishDrag);
    launcher.addEventListener('pointercancel', function () {
        drag = null;
        suppressClick = false;
    });
    launcher.addEventListener('click', function (event) {
        if (suppressClick) {
            suppressClick = false;
            event.preventDefault();
            event.stopImmediatePropagation();
        }
    }, true);

    function schedulePosition() {
        if (!scheduled) {
            scheduled = true;
            requestAnimationFrame(position);
        }
    }

    function position() {
        scheduled = false;
        if (!launcher.isConnected || launcher.hidden) {
            positionHint.hidden = true;
            return;
        }
        var viewportHeight = window.innerHeight;
        var defaultBottom = window.innerWidth <= 700 ? 16 : 24;
        var launcherSize = 44;
        var controls = [];
        document.querySelectorAll('button, a[href], input:not([type="hidden"]), textarea, select, [role="button"], .f-btn').forEach(function (element) {
            if (element === launcher || launcher.contains(element) || element.closest('.f-window')?.id === 'exampleDetailsWindow') {
                return;
            }
            var rect = element.getBoundingClientRect();
            if (rect.width > 0 && rect.height > 0 && rect.bottom > 0 && rect.top < viewportHeight
                && getComputedStyle(element).visibility !== 'hidden') {
                controls.push(rect);
            }
        });

        // 先沿右侧向上查找，再尝试左侧及中间各列；例如底栏从 606px 开始，入口下沿最多到 598px。
        // 无可用位置返回 null，不把“仍然盖住控件”的位置当成成功结果。
        function availableBottom(left) {
            var bottom = defaultBottom;
            for (var attempt = 0; attempt <= controls.length; attempt++) {
                var top = viewportHeight - bottom - launcherSize;
                var collision = controls.find(function (rect) {
                    return rect.right > left - gap && rect.left < left + launcherSize + gap
                        && rect.top < top + launcherSize + gap && rect.bottom > top - gap;
                });
                if (!collision) {
                    return bottom;
                }
                bottom = viewportHeight - collision.top + gap;
                if (bottom + launcherSize > viewportHeight - gap) {
                    return null;
                }
            }
            return null;
        }
        var rightmost = window.innerWidth - defaultBottom - launcherSize;
        var columns = [rightmost, defaultBottom];
        for (var left = rightmost - launcherSize - gap; left > defaultBottom; left -= launcherSize + gap) {
            columns.push(left);
        }
        var location = null;
        for (var column of columns) {
            var bottom = availableBottom(column);
            if (bottom !== null) {
                location = { right: window.innerWidth - column - launcherSize, bottom: bottom };
                break;
            }
        }
        // 全屏密集操作区没有空位时允许用户拖动入口，手机仍可触摸打开，桌面也保留键盘入口。
        launcher.classList.toggle('source-code-viewer-launcher-movable', !location);
        if (positionHint.hidden !== !!location) {
            positionHint.hidden = !!location;
        }
        if (!location) {
            // 手机横竖屏切换后，之前保存的 bottom 可能把入口推到屏幕上方，按新视口收回可见区域。
            // 只钳制越界部分，不把用户刚拖动的位置恢复为默认右下角。
            var current = launcher.getBoundingClientRect();
            var visibleLeft = Math.max(gap, Math.min(window.innerWidth - launcherSize - gap, current.left));
            var visibleTop = Math.max(gap, Math.min(viewportHeight - launcherSize - gap, current.top));
            location = {
                right: window.innerWidth - visibleLeft - launcherSize,
                bottom: viewportHeight - visibleTop - launcherSize,
            };
        }
        if (location) {
            for (var property of ['right', 'bottom']) {
                var value = location[property] + 'px';
                if (launcher.style[property] !== value) {
                    launcher.style[property] = value;
                }
            }
        }
    }

    window.addEventListener('resize', schedulePosition);
    document.addEventListener('scroll', schedulePosition, true);
    document.addEventListener('keydown', function (event) {
        if (document.activeElement === launcher && (event.key === 'Enter' || event.key === ' ')) {
            suppressClick = false;
        }
        if (event.altKey && event.shiftKey && !event.ctrlKey && !event.metaKey && event.code === 'KeyS' && !launcher.hidden) {
            event.preventDefault();
            suppressClick = false;
            launcher.click();
        }
    });
    var observer = new MutationObserver(function (records) {
        // 自己的位置更新不再触发测量，避免静止页面持续循环布局。
        if (records.some(function (record) {
            return record.target !== positionHint && (record.target !== launcher || record.attributeName === 'hidden');
        })) {
            schedulePosition();
        }
    });
    observer.observe(document.body, { childList: true, subtree: true, attributes: true, attributeFilter: ['class', 'style', 'hidden'] });
    schedulePosition();
}());
