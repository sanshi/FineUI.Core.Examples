using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class ClearSelectionModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = new[]
                {
                    new { Id = "a", Name = "甲" },
                    new { Id = "b", Name = "乙（不可选）" }
                };
                Grid1.DataBind();
            }
        }

        private void ClearAllSelections()
        {
            Grid1.ClearSelection();
            DropDownBox1.ClearSelection();
            DropDownList1.ClearSelection();
            DataList1.ClearSelection();
            Tree1.ClearSelection();
            CheckBoxList1.ClearSelection();
            RadioButtonList1.ClearSelection();
        }

        private void ShowSelection(string operation)
        {
            var counts = new[]
            {
                Grid1.SelectedRowIDArray.Length,
                DropDownBox1.Values.Length,
                DropDownList1.SelectedValueArray.Length,
                DataList1.SelectedValueArray.Length,
                Tree1.SelectedNodeIDArray.Length,
                CheckBoxList1.SelectedValueArray.Length,
                RadioButtonList1.SelectedValue == null ? 0 : 1
            };
            var cell = Grid1.SelectedCell;
            labResult.Text = operation
                + "：表格 " + counts[0]
                + "；下拉选择框 " + counts[1]
                + "；下拉列表 " + counts[2]
                + "；数据列表 " + counts[3]
                + "；树 " + counts[4]
                + "；复选框列表 " + counts[5]
                + "；单选框列表 " + counts[6]
                + "；单元格 " + (cell == null || cell.Length == 0 ? 0 : 1)
                + "；树勾选 " + Tree1.GetCheckedNodeIDs().Length;
        }

        protected void btnClearSelection_Click(object sender, EventArgs e)
        {
            ClearAllSelections();
            ShowSelection("清空后");
        }

        protected void btnClearAndSelect_Click(object sender, EventArgs e)
        {
            ClearAllSelections();

            // 同一轮清空后仍可重新选择，清空动作不能覆盖后面的业务赋值。
            Grid1.SelectedRowIDArray = new[] { "a" };
            DropDownBox1.Texts = new[] { "甲" };
            DropDownBox1.Values = new[] { "a" };
            DropDownList1.SelectedValue = "a";
            DataList1.SelectedValue = "a";
            Tree1.SelectedNodeID = "a";
            CheckBoxList1.SelectedValueArray = new[] { "a" };
            RadioButtonList1.SelectedValue = "a";
            ShowSelection("重新选中后");
        }

        protected void btnReadSelection_Click(object sender, EventArgs e)
        {
            ShowSelection("当前");
        }

        protected void btnUncheckNodes_Click(object sender, EventArgs e)
        {
            Tree1.UncheckAllNodes();
            ShowSelection("取消勾选后");
        }
    }
}
