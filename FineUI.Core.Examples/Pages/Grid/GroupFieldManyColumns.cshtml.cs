using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class GroupFieldManyColumnsModel : BaseModel
    {
        // 叶子列的表头文本（发货分组用全部 18 个，开票分组用前 17 个）
        private static readonly string[] SUB_LEAF_NAMES = new string[]
        {
            "金额", "数量", "单价", "去年同期", "去年变化率", "环比", "本年累计", "累计占比", "完成率",
            "目标", "差额", "预测", "实际", "偏差", "均价", "折扣", "税额", "备注"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }



        private void LoadData()
        {
            BuildColumns();

            Grid1.DataSource = GetDataTable();
            Grid1.DataBind();
        }

        // 循环生成全部 425 个叶子列：前 5 个锁定列 + 12 个月分组（每月下分「发货」18 列、「开票」17 列）
        // 序号列在页面标签里声明，排在所有列的最前面
        private void BuildColumns()
        {
            AddLockedColumn("客户编号", "khbh", 90);
            AddLockedColumn("客户名称", "khmc", 150);
            AddLockedColumn("产品", "product", 100);
            AddLockedColumn("期初", "qc", 90);
            AddLockedColumn("期末", "qm", 90);

            for (int mo = 1; mo <= 12; mo++)
            {
                GroupField fhGroup = new GroupField();
                fhGroup.HeaderText = "发货";
                fhGroup.TextAlign = TextAlign.Center;
                for (int i = 0; i < 18; i++)
                {
                    fhGroup.Columns.Add(CreateLeafField(SUB_LEAF_NAMES[i], String.Format("m{0}_fh_{1}", mo, i)));
                }

                GroupField kpGroup = new GroupField();
                kpGroup.HeaderText = "开票";
                kpGroup.TextAlign = TextAlign.Center;
                for (int i = 0; i < 17; i++)
                {
                    kpGroup.Columns.Add(CreateLeafField(SUB_LEAF_NAMES[i], String.Format("m{0}_kp_{1}", mo, i)));
                }

                GroupField monthGroup = new GroupField();
                monthGroup.HeaderText = mo + "月";
                monthGroup.TextAlign = TextAlign.Center;
                monthGroup.Columns.Add(fhGroup);
                monthGroup.Columns.Add(kpGroup);

                Grid1.Columns.Add(monthGroup);
            }
        }

        private void AddLockedColumn(string headerText, string dataField, int width)
        {
            RenderField field = new RenderField();
            field.HeaderText = headerText;
            field.DataField = dataField;
            field.Width = width;
            field.EnableLock = true;
            field.Locked = true;
            Grid1.Columns.Add(field);
        }

        private RenderField CreateLeafField(string headerText, string dataField)
        {
            RenderField field = new RenderField();
            field.HeaderText = headerText;
            field.DataField = dataField;
            field.Width = 90;
            field.TextAlign = TextAlign.Right;
            return field;
        }

        private DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("khbh", typeof(string)));
            table.Columns.Add(new DataColumn("khmc", typeof(string)));
            table.Columns.Add(new DataColumn("product", typeof(string)));
            table.Columns.Add(new DataColumn("qc", typeof(double)));
            table.Columns.Add(new DataColumn("qm", typeof(double)));
            for (int mo = 1; mo <= 12; mo++)
            {
                for (int i = 0; i < 18; i++)
                {
                    table.Columns.Add(new DataColumn(String.Format("m{0}_fh_{1}", mo, i), typeof(double)));
                }
                for (int i = 0; i < 17; i++)
                {
                    table.Columns.Add(new DataColumn(String.Format("m{0}_kp_{1}", mo, i), typeof(double)));
                }
            }

            Random rd = new Random();
            for (int r = 0; r < 20; r++)
            {
                DataRow row = table.NewRow();
                row["Id"] = 100 + r;
                row["khbh"] = "HT0100" + (r % 5 + 1);
                row["khmc"] = "客户" + r;
                row["product"] = "产品" + (r % 8);
                // 期初、期末与所有月份字段都填随机数值
                for (int c = 4; c < table.Columns.Count; c++)
                {
                    row[c] = Math.Round(rd.NextDouble() * 5000, 2);
                }

                table.Rows.Add(row);
            }

            return table;
        }


    }
}
