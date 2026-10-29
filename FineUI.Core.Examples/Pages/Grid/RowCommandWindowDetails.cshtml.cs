using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public class RowCommandWindowDetailsModel : PageBase
    {
        /// <summary>
        /// 当前客户标识，来自网址中的 slug 参数；未传入时为空字符串。
        /// </summary>
        public string CustomerSlug { get; private set; } = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            CustomerSlug = Request.Query["slug"].ToString();
        }
    }
}
