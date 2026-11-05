using System;

namespace FineUI.Core.Examples.Pages.GridRowGroup
{
    public partial class RowGroupAjaxModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }

        // 切换分组时重新提供学生数据，不能用上次请求的 DataSource。
        private void BindGroup(bool enabled, string field)
        {
            Grid1.EnableRowGroup = enabled;
            Grid1.DataRowGroupField = field;
            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }

        protected void btnGroupYear_Click(object sender, EventArgs e)
        {
            BindGroup(true, "EntranceYear");
        }

        protected void btnGroupMajor_Click(object sender, EventArgs e)
        {
            BindGroup(true, "Major");
        }

        protected void btnGroupOff_Click(object sender, EventArgs e)
        {
            BindGroup(false, Grid1.DataRowGroupField);
        }

        protected void btnReadGroup_Click(object sender, EventArgs e)
        {
            labGroup.Text = $"启用行分组：{Grid1.EnableRowGroup.ToString().ToLowerInvariant()}；分组字段：{Grid1.DataRowGroupField}";
        }
    }
}
