using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class FormModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 绑定表格
                BindGrid();
            }
        }


        // 删除表单当前对应的那一行
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable table = GetSourceData();
            DeleteRowByID(table, Convert.ToInt32(hfFormID.Text));

            // 把改过的数据写回Session
            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, table);

            // 重新绑定表格，并回到「新增」状态（等同于模拟点击新增按钮）
            BindGrid();
            RegisterStartupScript("onNewButtonClick();");
        }


        // 保存数据：表单里带行标识就是编辑，不带就是新增
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable table = GetSourceData();
            DataRow rowData;

            string strRowID = hfFormID.Text;
            if (String.IsNullOrEmpty(strRowID))
            {
                // 新增
                rowData = table.NewRow();

                // 设置行ID（模拟数据库的自增长列）
                rowData["Id"] = GetNextRowID(table);

                table.Rows.Add(rowData);
            }
            else
            {
                // 编辑
                int rowID = Convert.ToInt32(strRowID);
                rowData = FindRowByID(table, rowID);
            }

            // 把表单字段写回数据行
            rowData["Name"] = tbxFormUserName.Text.Trim();
            rowData["Gender"] = Convert.ToInt32(rblFormGender.SelectedValue);
            rowData["EntranceYear"] = Convert.ToInt32(nbFormEntranceYear.Text);
            rowData["EntranceDate"] = dpFormEntranceDate.Text;
            rowData["AtSchool"] = cbFormAtSchool.Checked;
            rowData["Major"] = ddlFormMajor.SelectedValue;

            // 把改过的数据写回Session
            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, table);

            // 重新绑定表格，并选中刚保存的那一行
            BindGrid();
            RegisterStartupScript(String.Format("F.ui.Grid1.selectRow('{0}');", rowData["Id"]));
        }


        #region BindGrid

        private void BindGrid()
        {
            Grid1.DataSource = GetSourceData();
            Grid1.DataBind();
        }

        #endregion


        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "Grid.Form";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        // 取数据时顺便记一下：Session里存的是JSON文本，每次取出来都是一个新对象，
        // 所以改完数据必须调用 SetObject 写回去，下一次取出来才会是改过的内容
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, DataSourceUtil.GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }

        #endregion
    }
}
