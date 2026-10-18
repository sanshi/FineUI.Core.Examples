using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class ExcelRowCommandDownloadCRCModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }

        // 本页负责界面与控件，独立文件或数据响应由对应服务入口处理。
    }
}
