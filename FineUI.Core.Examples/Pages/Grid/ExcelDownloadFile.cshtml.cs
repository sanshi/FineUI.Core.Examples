using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class ExcelDownloadFileModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }

        // 本页负责界面与控件，独立文件或数据响应由对应服务入口处理。
    }
}
