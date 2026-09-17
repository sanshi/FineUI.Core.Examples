using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineUI.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Panel
{
    public partial class PanelModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        private void LoadData()
        {
            Panel2.Content = "可以在此放置<a href=\"http://www.w3schools.com/html/\" target=\"_blank\">HTML</a>标签。";
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("面板处于{0}状态", Panel1.Collapsed ? "折叠" : "展开"));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel2.Collapsed = !Panel2.Collapsed;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Panel1.Title = String.Format("面板（{0}）", DateTime.Now.ToLongTimeString());
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ToolbarText1.Text = String.Format("工具条文本一（{0}）", DateTime.Now.ToLongTimeString());
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ToolbarText1.Hidden = !ToolbarText1.Hidden;
            ToolbarSeparator1.Hidden = !ToolbarSeparator1.Hidden;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Toolbar1.Hidden = true;
        }


        protected void Button8_Click(object sender, EventArgs e)
        {
            Toolbar1.Hidden = false;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            if (Panel1.IconFont == IconFont._VolumeUp)
            {
                Panel1.IconFont = IconFont._VolumeDown;
            }
            else if (Panel1.IconFont == IconFont._VolumeDown)
            {
                Panel1.IconFont = IconFont._VolumeOff;
            }
            else
            {
                Panel1.IconFont = IconFont._VolumeUp;
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Panel1.IconFont = IconFont.None;
        }

    }
}