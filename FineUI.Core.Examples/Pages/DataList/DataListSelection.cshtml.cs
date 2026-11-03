using System;

namespace FineUI.Core.Examples.Pages.DataList
{
    public partial class DataListSelectionModel : PageBase
    {
        private void ShowSelection()
        {
            labResult.Text = "单选：" + (DataListSingle.SelectedValue ?? "")
                + "；多选：" + String.Join(", ", DataListMulti.SelectedValueArray)
                + "；保持选择：" + String.Join(", ", DataListKeep.SelectedValueArray);
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            ShowSelection();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            DataListSingle.SelectedValue = "guangzhou";
            DataListMulti.SelectedValueArray = new[] { "shanghai", "guangzhou" };
            DataListKeep.SelectedValueArray = new[] { "shanghai", "guangzhou" };
            ShowSelection();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            DataListSingle.ClearSelection();
            DataListMulti.ClearSelection();
            DataListKeep.ClearSelection();
            ShowSelection();
        }
    }
}
