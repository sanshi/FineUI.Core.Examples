using System;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class DisplaySelectionModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Grid1.EnableCheckBoxSelect = true;
            Grid1.EnableMultiSelect = true;
            Grid1.EnableRowLines = false;
            Grid1.EnableAlternateRowColor = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.EnableCheckBoxSelect = false;
            Grid1.EnableMultiSelect = false;
            Grid1.EnableRowLines = true;
            Grid1.EnableAlternateRowColor = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LabelResult.Text = $"复选框选择列：{Grid1.EnableCheckBoxSelect}；多选：{Grid1.EnableMultiSelect}；"
                + $"行分隔线：{Grid1.EnableRowLines}；交替行颜色：{Grid1.EnableAlternateRowColor}";
        }

        protected void BusinessButton_Click(object sender, EventArgs e)
        {
            LabelResult.Text = "业务按钮仍可回发。";
        }
    }
}
