using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class MenuDynamicButtonModel : BaseModel
    {
        protected FineUI.Core.Button btnDynamic;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
            else if (btnDynamic != null)
            {
                // 动态按钮的当前状态已恢复；点击方法由服务器每轮重新登记，不能从浏览器取回。
                btnDynamic.OnClick = new Event("click", "btnDynamic_Click");
            }
        }

        private void LoadData()
        {
            FineUI.Core.Button btn = new FineUI.Core.Button();
            btn.ID = "btnDynamic";
            btn.Text = "工具栏中的按钮数（动态添加的按钮）";
            btn.OnClick = new Event("click", "btnDynamic_Click");
            Toolbar1.Items.Add(btn);
        }

        
        protected void btnDynamic_Click(object sender, EventArgs e)
        {
            ShowNotify("工具栏中的按钮数：" + Toolbar1.Items.Count);
        }


    }
}
