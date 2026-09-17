using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class DataBindSimpleListModel : BaseModel
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
            List<string> strList = new List<string>();
            strList.Add("可选项1");
            strList.Add("可选项2");
            strList.Add("可选项3");
            strList.Add("可选项4");
            strList.Add("可选项5");
            strList.Add("可选项6");
            strList.Add("可选择项7");
            strList.Add("可选择项8");
            strList.Add("可选择项9");
            strList.Add("这是一个很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长的可选项");

            DropDownList1.DataSource = strList;
            DropDownList1.DataBind();
        }

        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.Text))
            {
                labResult.Text = String.Format("选中项：{0}（值：{1}）", DropDownList1.Text, DropDownList1.SelectedValue);
            }
            else
            {
                labResult.Text = "无选中项";
            }
        }

        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "可选项6";
        }

    }
}