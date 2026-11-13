/**
 * 通用通知辅助函数，供移动示例、错误提示及其他业务页面显示居中的短消息。
 * 页面引入 res/js/common.js，再调用 showCenterNotify(message, messageIcon)。
 * 本文件只声明函数，不会在加载时创建通知；通知显示三秒，也可点击遮罩关闭。
 */

/**
 * 在当前页面中央显示带遮罩的短消息，三秒后自动关闭。
 * 调用示例：showCenterNotify('保存完成', 'success')；message 为正文，messageIcon 可省略，或用 information、warning、question、error、success。
 */
function showCenterNotify(message, messageIcon) {
    F.notify({
        message: message,
        messageIcon: messageIcon || '',
        modal: true,
        hideOnMaskClick: true,
        header: false,
        displayMilliseconds: 3000,
        positionX: 'center',
        positionY: 'center',
        messageAlign: 'center',
        minWidth: 200
    });
}
