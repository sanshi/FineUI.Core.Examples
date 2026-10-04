using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FineUI.Core.Examples.Pages.Grid
{
    // 独立 HTTP 响应入口：不渲染 FineUI 页面，也不初始化页面主题等配置。
    public class ExcelRowCommandDownloadCRCExportModel : PageModel
    {
        // 本入口只接收 POST；GET 不应返回空白页面。
        public IActionResult OnGet()
        {
            Response.Headers["Allow"] = "POST";
            return StatusCode(405);
        }

        public IActionResult OnPost(JObject content)
        {
            string rowId = content.Value<string>("id");
            int rowIndex = content.Value<int>("index");
            JObject rowValues = content.Value<JObject>("values");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("你点击了第 {0} 行，数据如下：", rowIndex + 1);
            sb.AppendLine();
            sb.AppendFormat("ID：{0}", rowId);
            sb.AppendLine();
            sb.AppendFormat("姓名：{0}", rowValues.Value<string>("Name"));
            sb.AppendLine();
            sb.AppendFormat("性别：{0}", rowValues.Value<string>("Gender") == "1" ? "男" : "女");
            sb.AppendLine();
            sb.AppendFormat("入学年份：{0}", rowValues.Value<string>("EntranceYear"));
            sb.AppendLine();
            sb.AppendFormat("是否在校：{0}", rowValues.Value<bool>("AtSchool") ? "是" : "否");
            sb.AppendLine();
            sb.AppendFormat("所学专业：{0}", rowValues.Value<string>("Major"));
            sb.AppendLine();
            sb.AppendFormat("分组：{0}", rowValues.Value<string>("Group"));
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/plain", "row_" + rowId + ".txt");
        }
    }
}
