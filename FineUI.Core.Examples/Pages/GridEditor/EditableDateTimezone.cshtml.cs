using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FineUI.Core.Examples.Pages.GridEditor
{
    public partial class EditableDateTimezoneModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = GetSourceData();
                Grid1.DataBind();
            }
        }

        // 构造演示数据：PlanDate 故意使用带 +00:00 偏移的 UTC ISO 字符串，
        // 在东八区客户端会被 new Date(...) 解析为本地 08:00（历史上会导致“重选同一天也被标记为已修改”，本例用于验证已修复）。
        private DataTable GetSourceData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("PlanDate", typeof(string));

            table.Rows.Add(1, "张三", "2026-06-19T00:00:00+00:00");
            table.Rows.Add(2, "李四", "2026-02-28T00:00:00+00:00");
            table.Rows.Add(3, "王五", "2026-12-01T00:00:00+00:00");

            return table;
        }
    }
}
