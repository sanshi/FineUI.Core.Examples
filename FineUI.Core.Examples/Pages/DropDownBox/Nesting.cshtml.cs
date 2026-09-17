using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownBox
{
    public partial class NestingModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }



        private void BindGrid()
        {
            Grid1.DataSource = GetFilteredData(ddlGender.SelectedValue, ddbMajor.Values);
            Grid1.DataBind();
        }


        private DataTable GetFilteredData(string gender, string[] majors)
        {
            DataTable table = DataSourceUtil.GetDataTable();

            DataView view = table.DefaultView;

            List<string> filters = new List<string>();

            // 性别过滤
            if (!String.IsNullOrEmpty(gender))
            {
                filters.Add(String.Format("Gender = {0}", gender));
            }

            // 专业过滤
            if (majors != null && majors.Length > 0)
            {
                List<string> majorFilters = new List<string>();
                foreach (string ddb in majors)
                {
                    majorFilters.Add(String.Format("Major = '{0}'", ddb));
                }
                filters.Add("(" + String.Join(" OR ", majorFilters.ToArray()) + ")");
            }

            if (filters.Count > 0)
            {
                view.RowFilter = String.Join(" AND ", filters.ToArray());
            }

            return table;
        }



        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void ddbMajor_TextChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownBox1.Text))
            {
                labResult.Text = String.Format("下拉框文本：{0}（值：{1}）", DropDownBox1.Text, String.Join(", ", DropDownBox1.Values));
            }
            else
            {
                labResult.Text = "下拉框为空";
            }
        }


    }
}