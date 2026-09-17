using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TwinTriggerBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void TwinTriggerBox1_Trigger2Click(object sender, EventArgs e)
        {
            // 点击 TwinTriggerBox 的搜索按钮
            if (!String.IsNullOrEmpty(TwinTriggerBox1.Text))
            {
                // 执行搜索动作
                ShowNotify(String.Format("在关键词“{0}”中搜索", TwinTriggerBox1.Text));

                TwinTriggerBox1.ShowTrigger1 = true;
            }
            else
            {
                ShowNotify("请输入你要搜索的关键词！");
            }

        }


        protected void TwinTriggerBox1_Trigger1Click(object sender, EventArgs e)
        {
            // 点击 TwinTriggerBox 的取消按钮
            ShowNotify("取消搜索！");

            // 执行清空动作
            TwinTriggerBox1.Text = "";
            TwinTriggerBox1.ShowTrigger1 = false;
        }

        
    }
}