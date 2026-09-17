using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class SimulateTreeModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        #region LoadData

        public class JQueryFeature
        {
            private string _id;

            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private int _level;

            public int Level
            {
                get { return _level; }
                set { _level = value; }
            }

            private bool _enableSelect;

            public bool EnableSelect
            {
                get { return _enableSelect; }
                set { _enableSelect = value; }
            }

            public JQueryFeature(string id, string name, int level, bool enableSelect)
            {
                _id = id;
                _name = name;
                _level = level;
                _enableSelect = enableSelect;
            }

            public override string ToString()
            {
                return String.Format("Name:{0}+Id:{1}", Name, Id);
            }
        }

        private void LoadData()
        {
            List<JQueryFeature> myList = new List<JQueryFeature>();
            myList.Add(new JQueryFeature("0", "jQuery", 0, false));
            myList.Add(new JQueryFeature("1", "核心", 1, false));
            myList.Add(new JQueryFeature("2", "选择符", 1, false));
            myList.Add(new JQueryFeature("3", "基本选择符", 2, true));
            myList.Add(new JQueryFeature("4", "内容选择符", 2, true));
            myList.Add(new JQueryFeature("5", "属性选择符", 2, true));
            myList.Add(new JQueryFeature("6", "筛选", 1, false));
            myList.Add(new JQueryFeature("7", "过滤", 2, true));
            myList.Add(new JQueryFeature("8", "查找", 2, true));
            myList.Add(new JQueryFeature("9", "事件", 1, false));
            myList.Add(new JQueryFeature("10", "页面载入", 2, true));
            myList.Add(new JQueryFeature("11", "事件处理", 2, true));
            myList.Add(new JQueryFeature("12", "事件委托", 2, true));


            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataSimulateTreeLevelField = "Level";
            DropDownList1.DataEnableSelectField = "EnableSelect";
            DropDownList1.DataSource = myList;
            DropDownList1.DataBind();

            DropDownList1.SelectedValue = "3";
        }

        #endregion

        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.Text))
            {
                labResult.Text = String.Format("选中项：{0}（值：{1}）", DropDownList1.Text, DropDownList1.SelectedValue);
            }
            else
            {
                labResult.Text = "无选中项";
            }
        }

        #region btnDataBind_Click

        protected void btnDataBind_Click(object sender, EventArgs e)
        {
            // 绑定数据源
            DropDownList1.DataSource = GetData2();
            DropDownList1.DataBind();

            // 设置选中项
            DropDownList1.SelectedValue = "11";
        }


        private List<JQueryFeature> GetData2()
        {
            List<JQueryFeature> myList = new List<JQueryFeature>();
            myList.Add(new JQueryFeature("0", "jQuery - 2", 0, false));
            myList.Add(new JQueryFeature("1", "核心 - 2", 1, false));
            myList.Add(new JQueryFeature("6", "筛选 - 2", 1, false));
            myList.Add(new JQueryFeature("7", "过滤 - 2", 2, true));
            myList.Add(new JQueryFeature("8", "查找 - 2", 2, true));
            myList.Add(new JQueryFeature("9", "事件 - 2", 1, false));
            myList.Add(new JQueryFeature("10", "页面载入 - 2", 2, true));
            myList.Add(new JQueryFeature("11", "事件处理 - 2", 2, true));
            myList.Add(new JQueryFeature("12", "事件委托 - 2", 2, true));

            return myList;
        }

        #endregion

    }
}