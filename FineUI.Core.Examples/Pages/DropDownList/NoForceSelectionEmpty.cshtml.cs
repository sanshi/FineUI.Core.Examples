using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class NoForceSelectionEmptyModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 设置下拉列表的初始值为自定义文本
                DropDownList1.Text = "初始自定义值";
            }
        }


        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                labResult.Text = String.Format("选中项：{0}（值：{1}）", DropDownList1.Text, DropDownList1.SelectedValue);
            }
            else
            {
                labResult.Text = String.Format("用户输入值：{0}", DropDownList1.Text);
            }
        }

        protected void btnRebindData_Click(object sender, EventArgs e)
        {
            // PersistItems 默认为 true，回发时客户端把当前列表项随 F_STATE 带回，Items.Count 即为
            // 点击时下拉列表的真实项数——按此在“绑定 9 项”与“清空”间切换（无需从客户端另取参数）
            if (DropDownList1.Items.Count == 0)
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

                DropDownList1.DataSource = strList;
                DropDownList1.DataBind();
            }
            else
            {
                DropDownList1.DataSource = null;
                DropDownList1.DataBind();
            }
        }


        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownList1.Text = "用户输入值";
        }

    }
}