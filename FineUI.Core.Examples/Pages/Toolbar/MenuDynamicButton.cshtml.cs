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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            FineUI.Core.Button btn = new FineUI.Core.Button();
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