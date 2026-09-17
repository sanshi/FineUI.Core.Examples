using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormDynamicModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        // 这两个控件是动态创建的，不会在 .designer.cs 文件中生成，只需要在此定义即可
        protected FineUI.Core.TextBox tbxUserName;
        protected FineUI.Core.DropDownList ddlGender;


        private void LoadData()
        {
            var tbxUser = new FineUI.Core.TextBox();
            tbxUser.ID = "tbxUserName";
            tbxUser.Text = "";
            tbxUser.Label = "用户名";
            tbxUser.ShowLabel = true;
            tbxUser.ShowRedStar = true;
            tbxUser.Required = true;
            tbxUser.EmptyText = "请输入用户名";
            FormRow1.Items.Add(tbxUser);

            var ddlGender = new FineUI.Core.DropDownList();
            ddlGender.ID = "ddlGender";
            ddlGender.Label = "性别（回发事件）";
            ddlGender.Items.Add("男", "0");
            ddlGender.Items.Add("女", "1");
            ddlGender.AutoSelectFirstItem = false;
            // 添加后台事件处理函数
            ddlGender.Events.Add(new Event("change", "ddlGender_SelectedIndexChanged"));
            FormRow1.Items.Add(ddlGender);
        }


        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowNotify("选择的性别：" + ddlGender.Text);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify("用户名：" + tbxUserName.Text + "  性别：" + ddlGender.Text);
        }


        


    }
}