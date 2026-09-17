using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class TwoGridModel : BaseModel
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
            DataTable classTable = DataSourceUtil.GetClassDataTable();
            int selectedRowId = (int)classTable.Rows[0]["Id"];

            Grid2.DataSource = classTable;
            Grid2.DataBind();

            Grid2.SelectedRowID = selectedRowId.ToString();
            labelClassDesc.Text = GetClassDesc(selectedRowId, classTable);

            Grid1.DataSource = GetClassDetailTable(selectedRowId);
            Grid1.DataBind();
        }


        private string GetClassDesc(int classId, DataTable classTable)
        {
            foreach (DataRow row in classTable.Rows)
            {
                int currentClassId = (int)row["Id"];
                if (classId == currentClassId)
                {
                    return "班级描述：" + row["Desc"].ToString();
                }
            }

            return String.Empty;
        }

        private DataTable GetClassDetailTable(int classId)
        {
            DataTable table = null;
            if (classId == 101)
            {
                table = DataSourceUtil.GetDataTable();
            }
            else
            {
                table = DataSourceUtil.GetDataTable2();
            }

            return table;
        }


        protected void Grid2_RowSelect(object sender, GridRowEventArgs e)
        {
            var selectedRowId = Convert.ToInt32(Grid2.SelectedRowID);

            DataTable classTable = DataSourceUtil.GetClassDataTable();
            labelClassDesc.Text = GetClassDesc(selectedRowId, classTable);

            Grid1.DataSource = GetClassDetailTable(selectedRowId);
        }

    }
}