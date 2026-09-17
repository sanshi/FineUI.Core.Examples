using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineUI.Core.Examples
{
    public class BaseMobileModel : BaseModel
    {
        #region static readonly

        protected static readonly string DATALIST_ITEM_TEMPLATE = "<table class=\"item-table\"><tr><td><img class=\"item-img\" src=\"{0}\"><div class=\"item-text\">{1}</div><div class=\"item-desc\">{2}</div></td></tr></table>";

        protected static readonly string DATALIST_SIMPLE_ITEM_TEMPLATE = "<table class=\"item-table\"><tr><td><img class=\"item-img\" src=\"{0}\"><div class=\"item-text\">{1}</div></td></tr></table>";

        #endregion

        #region ShowNotify

        // 移动端通知：构建一个统一配置的 Notify（消息由调用方分别按普通文本 / 可信 HTML 设置）
        private Notify CreateMobileNotify(MessageBoxIcon messageIcon)
        {
            Notify notify = new Notify();
            notify.Target = Target.Self;
            notify.ShowHeader = false;
            notify.PositionX = Position.Center;
            notify.PositionY = Position.Center;
            notify.MessageBoxIcon = messageIcon;
            notify.MessageAlign = TextAlign.Center;
            notify.MinWidth = 200;
            notify.DisplayMilliseconds = 3000;
            notify.IsModal = true;
            notify.HideOnMaskClick = true;
            return notify;
        }

        /// <summary>
        /// 显示通知对话框（普通文本，HTML 转义）
        /// </summary>
        public override void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            Notify notify = CreateMobileNotify(messageIcon);
            notify.Message = message;
            notify.Show();
        }

        /// <summary>
        /// 显示通知对话框（可信 HTML，原样输出不转义）
        /// </summary>
        public override void ShowNotify(RawHtml message, MessageBoxIcon messageIcon)
        {
            Notify notify = CreateMobileNotify(messageIcon);
            notify.MessageRawHtml = message;
            notify.Show();
        }


        #endregion
    }
}
