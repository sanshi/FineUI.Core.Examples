using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Text;

namespace FineUI.Core.Examples.Pages.Grid
{
    // 独立 HTTP 响应入口：不渲染 FineUI 页面，也不初始化页面主题等配置。
    public class ExcelExportModel : PageModel
    {
        // 本入口只接收 POST；GET 不应返回空白页面。
        public IActionResult OnGet()
        {
            Response.Headers["Allow"] = "POST";
            return StatusCode(405);
        }

        public IActionResult OnPost()
        {
            DataTable source = DataSourceUtil.GetDataTable();

            string TH_HTML = "<th>{0}</th>";
            string TD_HTML = "<td>{0}</td>";
            string TD_IMAGE_HTML = "<td><img src=\"{0}\"></td>";

            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"Content-Type\" content=\"application/vnd.ms-excel;charset=utf-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");

            sb.Append("<tr>");
            sb.AppendFormat(TH_HTML, "");
            sb.AppendFormat(TH_HTML, "姓名");
            sb.AppendFormat(TH_HTML, "性别");
            sb.AppendFormat(TH_HTML, "入学年份");
            sb.AppendFormat(TH_HTML, "是否在校");
            sb.AppendFormat(TH_HTML, "所学专业");
            sb.AppendFormat(TH_HTML, "分组");
            sb.AppendFormat(TH_HTML, "注册日期");
            sb.Append("</tr>");

            int rowIndex = 1;
            foreach (DataRow row in source.Rows)
            {
                sb.Append("<tr>");
                sb.AppendFormat(TD_HTML, rowIndex++);
                sb.AppendFormat(TD_HTML, row["Name"]);
                sb.AppendFormat(TD_HTML, row["Gender"].ToString() == "1" ? "男" : "女");
                sb.AppendFormat(TD_HTML, row["EntranceYear"]);
                sb.AppendFormat(TD_HTML, (bool)row["AtSchool"] ? "√" : "×");
                sb.AppendFormat(TD_HTML, row["Major"]);
                sb.AppendFormat(TD_IMAGE_HTML, UrlUtil.GetAbsoluteUrl(Request, Url, String.Format("~/res/images/16/{0}.png", row["Group"])));
                sb.AppendFormat(TD_HTML, ((DateTime)row["LogTime"]).ToString("yyyy/MM/dd"));
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "application/vnd.ms-excel", "myexcel.xls");
        }
    }
}
