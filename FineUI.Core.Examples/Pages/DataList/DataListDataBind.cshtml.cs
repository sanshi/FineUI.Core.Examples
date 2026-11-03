using System;

namespace FineUI.Core.Examples.Pages.DataList
{
    public partial class DataListDataBindModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataList1.DataSource = new[]
                {
                    new { Id = "beijing", Name = "北京" },
                    new { Id = "shanghai", Name = "上海" },
                    new { Id = "guangzhou", Name = "广州" }
                };
                DataList1.DataBind();
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            DataList1.LoadData(
                new DataListItem("广州", "guangzhou"),
                new DataListItem("上海", "shanghai"),
                new DataListItem("北京", "beijing"));
            hfBatch.Text = "0";
            labResult.Text = "数据数量：3";
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            // 批次在回发间保留，例如两次追加生成 shenzhen-1 和 shenzhen-2，避免重复项值。
            int batch = Convert.ToInt32(hfBatch.Text) + 1;
            DataList1.AppendData(
                new DataListItem("深圳 " + batch, "shenzhen-" + batch),
                new DataListItem("杭州 " + batch, "hangzhou-" + batch));
            hfBatch.Text = batch.ToString();
            labResult.Text = "数据数量：" + (3 + batch * 2);
        }
    }
}
