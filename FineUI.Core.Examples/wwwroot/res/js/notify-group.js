/**
 * 把连续弹出的通知按高度排成一组，关闭其中一条时自动补齐空位。
 * 由消息分类中的通知分组示例按需引入，并配合 notify-group 对应样式。
 * 页面调用 showNotifyGroup(options, newestOnTop)，options 是 F.notify 的配置；true 表示新消息放最上方，否则追加到底部。
 * 本文件维护当前页面自己的通知队列，引入时不自动弹出消息。
 */

// 通知对话框分组
(function () {

    // _notifySpace: 消息框之间的间距
    // _notifies: 存放当前正在显示的对话框列表
    var _orderNumber = 1, _notifySpace = 5, _notifies = [];

    /**
     * 通知关闭时移出队列，把后续通知向上移动，消除留下的空位。
     * 由 showNotifyGroup 登记为通知 hide 回调；this 是正在关闭的通知实例。
     */
    function onNotifyHide() {
        // 先清空之前尚未完成的动画
        clearNotifiesAnimation();

        var notify = this;
        var notifyHeight = notify.el.outerHeight(true) + _notifySpace;
        var notifyIndex = $.inArray(notify, _notifies);
        _notifies.splice(notifyIndex, 1);

        var count = _notifies.length;
        if (count) {
            for (var i = notifyIndex; i < count; i++) {
                var item = _notifies[i];
                item.top -= notifyHeight;
                item.el.animate({
                    'top': item.top
                });
            }

            // 按照 notify.top 重新排序
            _notifies.sort(function (a, b) {
                return a.top - b.top;
            });
        }
    }

    /**
     * 在顶部插入新通知前，把已有通知按新消息高度整体下移。
     * 内部调用 moveNotifiesDown(newNotify, fn)；所有移动完成后执行 fn，再显示新消息，避免重叠。
     */
    function moveNotifiesDown(newNotify, fn) {
        // 先清空之前尚未完成的动画
        clearNotifiesAnimation();

        var count = _notifies.length, finished = 0;
        if (!count) {
            fn.apply(window);
            return;
        }

        var notifyHeight = newNotify.el.outerHeight(true) + _notifySpace;
        for (var i = 0; i < count; i++) {
            var item = _notifies[i];
            item.top += notifyHeight;
            item.el.animate({
                'top': item.top
            }, function () {
                // 动画完成后执行的函数
                finished++;

                if (finished >= count) {
                    fn.apply(window);
                }
            });
        }
    }

    /**
     * 完成队列里尚未结束的位移动画，再按最终位置计算下一次排布。
     * 由添加和关闭通知的内部流程调用，避免用户快速操作时使用动画中间位置。
     */
    function clearNotifiesAnimation() {
        var count = _notifies.length;
        if (count) {
            for (var i = 0; i < count; i++) {
                var item = _notifies[i];
                var itemEl = item.el;
                if (itemEl.is(":animated")) {
                    itemEl.stop(false, true);
                }
            }
        }
    }

    /**
     * 计算追加到底部的新通知位置，包含已有消息高度和消息间距。
     * 内部调用 calcNotifyTop()；队列为空时返回顶部预留的间距。
     */
    function calcNotifyTop() {
        var top = _notifySpace;
        if (_notifies.length) {
            var lastNotify = _notifies[_notifies.length - 1];
            top += lastNotify.top + lastNotify.el.outerHeight(true);
        }
        return top;
    }

    /**
     * 创建一条分组通知，并根据插入方向维护已有通知的位置。
     * 调用示例：showNotifyGroup({ message: '保存完成' }, true)。
     * 会更新传入 options 的定位、样式和 hide 监听；newestOnTop 为 true 时先等待旧消息下移，再显示新消息。
     */
    window.showNotifyGroup = function (options, newestOnTop) {
        // 创建一个消息对话框实例
        $.extend(options, {
            cls: 'notify-group-item', // 自定义样式
            constrainInitialSize: false, // 不限制初始大小，因为本示例的消息框可能会超出页面边界
            positionX: 'right',
            listeners: {
                hide: onNotifyHide
            }
        });

        if (newestOnTop) {
            // 最新的显示在最上方，需要先隐藏，等 moveNotifiesDown 之后再显示
            options.hidden = true;
            options.top = _notifySpace;
        } else {
            options.top = calcNotifyTop();
        }

        var notify = F.notify(options);

        if (newestOnTop) {
            moveNotifiesDown(notify, function () {
                notify.show();
            });
            _notifies.splice(0, 0, notify);
        } else {
            _notifies.push(notify);
        }
    }


})();