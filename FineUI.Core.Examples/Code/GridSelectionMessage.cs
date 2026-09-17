using System;
using System.Text;
using System.Web;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 行选择示例共用：把表格「当前选中行的数据」拼成一段可信 HTML（各数据键值的表格），
    /// 供服务端按钮弹通知。入参自足、不碰页面上下文，所以是静态工具类而不是页面基类的成员。
    /// 表格结构由本函数生成，数据键名与键值一律 HTML 编码后再拼——它们来自客户端回传的
    /// __FSTATE（不重新绑定数据的页面尤其明显），既可能含 &amp; &lt; 把表格渲染坏，也不该当可信内容用。
    /// </summary>
    public static class GridSelectionMessage
    {
        /// <summary>
        /// 生成「共选中了 N 行」的 HTML 表格；未选中返回提示文本。
        /// 数据由Grid.GetSelectedDataKeys按稳定行ID关联，不依赖行序号。
        /// </summary>
        public static string HowManyRowsAreSelected(Grid grid)
        {
            var localizer = FineUI.Core.PageContext.GetLocalizer<SharedResources>();

            StringBuilder sb = new StringBuilder();
            var selectedDataKeys = grid.GetSelectedDataKeys();
            int selectedCount = selectedDataKeys.Count;
            if (selectedCount > 0)
            {
                sb.Append("<p><strong>" + localizer.GetString("GridSelectedRowsMessage", selectedCount) + "</strong></p>");
                sb.Append("<table class=\"result\">");

                sb.Append("<tr>");
                foreach (string datakey in grid.DataKeyNames)
                {
                    sb.AppendFormat("<th>{0}</th>", HttpUtility.HtmlEncode(datakey));
                }
                sb.Append("</tr>");

                foreach (object[] dataKeys in selectedDataKeys)
                {
                    sb.Append("<tr>");
                    for (int j = 0; j < dataKeys.Length; j++)
                    {
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(Convert.ToString(dataKeys[j])));
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Append("<strong>" + localizer.GetString("GridNoSelectionMessage") + "</strong>");
            }

            return sb.ToString();
        }
    }
}
