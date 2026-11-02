using System;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class DynamicEventRevocationModel : PageBase
    {
        private const string CountSessionKey = "DynamicEventRevocation.Count";

        protected FineUI.Core.Button btnDynamic;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpContext.Session.SetInt32(CountSessionKey, 0);
                LoadData();
            }
            else if (cbxRevoke.Checked && btnDynamic != null)
            {
                // 即使本轮没有重新 Add，也可以明确撤回 click，阻止浏览器保留的旧事件触发后台方法。
                btnDynamic.Events.Remove(new Event("click", "btnDynamic_Click"));
            }
        }

        private void LoadData()
        {
            btnDynamic = new FineUI.Core.Button
            {
                ID = "btnDynamic",
                Text = "触发动态按钮事件"
            };
            btnDynamic.Events.Add(new Event("click", "btnDynamic_Click"));
            Panel1.Items.Add(btnDynamic);
        }

        private int ReadCount()
        {
            // 计数从服务器会话读取，便于在拒绝回发后独立核对，而不是依赖浏览器标签上的文字。
            return HttpContext.Session.GetInt32(CountSessionKey) ?? 0;
        }

        protected void btnDynamic_Click(object sender, EventArgs e)
        {
            int count = ReadCount() + 1;
            HttpContext.Session.SetInt32(CountSessionKey, count);
            labCount.Text = "服务器处理次数：" + count;
            labResult.Text = "动态按钮事件已执行";
        }

        protected void btnReadCount_Click(object sender, EventArgs e)
        {
            labCount.Text = "服务器处理次数：" + ReadCount();
            labResult.Text = "已读取服务器处理次数";
        }
    }
}
