using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class CheckBoxListUpdateModel : BaseModel
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
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("value1", "可选项 1"));
            myList.Add(new TestClass("value2", "可选项 2"));
            myList.Add(new TestClass("value3", "可选项 3"));
            myList.Add(new TestClass("value4", "可选项 4"));
            myList.Add(new TestClass("value5", "可选项 5"));
            myList.Add(new TestClass("value6", "可选项 6"));

            CheckBoxList2.DataTextField = "Name";
            CheckBoxList2.DataValueField = "Id";
            CheckBoxList2.DataSource = myList;
            CheckBoxList2.DataBind();

            CheckBoxList2.SelectedValueArray = new string[] { "value1", "value2", "value3" };
        }

        #region TestClass

        public class TestClass
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

            public TestClass(string id, string name)
            {
                _id = id;
                _name = name;
            }

        }


        #endregion

        protected void btnCheckedItemsList1_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表一的选中项：{0}", String.Join(", ", CheckBoxList1.SelectedValueArray)));
        }

        protected void btnCheckedItemsList2_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表二的选中项：{0}", String.Join(", ", CheckBoxList2.SelectedValueArray)));
        }

        protected void btnCheckedItemsList3_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表三的选中项：{0}", String.Join(", ", CheckBoxList3.SelectedValueArray)));
        }

        protected void btnCheckedItemsList4_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表四的选中项：{0}", String.Join(", ", CheckBoxList4.SelectedValueArray)));
        }


        protected void btnUpdateList1_Click(object sender, EventArgs e)
        {
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("value1", "数据绑定值 1"));
            myList.Add(new TestClass("value2", "数据绑定值 2"));
            myList.Add(new TestClass("value3", "数据绑定值 3"));
            myList.Add(new TestClass("value4", "数据绑定值 4"));

            CheckBoxList1.DataTextField = "Name";
            CheckBoxList1.DataValueField = "Id";
            CheckBoxList1.DataSource = myList;
            CheckBoxList1.DataBind();

            CheckBoxList1.SelectedValueArray = new string[] { "value1", "value3" };
        }


        protected void btnUpdateList2_Click(object sender, EventArgs e)
        {
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("value1", "数据绑定值 1"));
            myList.Add(new TestClass("value2", "数据绑定值 2"));
            myList.Add(new TestClass("value3", "数据绑定值 3"));
            myList.Add(new TestClass("value4", "数据绑定值 4"));
            myList.Add(new TestClass("value5", "数据绑定值 5"));
            myList.Add(new TestClass("value6", "数据绑定值 6"));

            CheckBoxList2.DataTextField = "Name";
            CheckBoxList2.DataValueField = "Id";
            CheckBoxList2.DataSource = myList;
            CheckBoxList2.DataBind();

            CheckBoxList2.SelectedValueArray = new string[] { "value1", "value3", "value6" };
        }


        protected void btnUpdateList3_Click(object sender, EventArgs e)
        {
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("value1", "数据绑定值 1"));
            myList.Add(new TestClass("value2", "数据绑定值 2"));
            myList.Add(new TestClass("value3", "数据绑定值 3"));
            myList.Add(new TestClass("value4", "数据绑定值 4"));
            myList.Add(new TestClass("value5", "数据绑定值 5"));
            myList.Add(new TestClass("value6", "数据绑定值 6"));
            myList.Add(new TestClass("value7", "数据绑定值 7"));
            myList.Add(new TestClass("value8", "数据绑定值 8"));
            myList.Add(new TestClass("value9", "数据绑定值 9"));

            CheckBoxList3.DataTextField = "Name";
            CheckBoxList3.DataValueField = "Id";
            CheckBoxList3.DataSource = myList;
            CheckBoxList3.DataBind();

            CheckBoxList3.SelectedValueArray = new string[] { "value1", "value2", "value6", "value7" };
        }

        protected void btnUpdateList4_Click(object sender, EventArgs e)
        {
            if (CheckBoxList4.Items.Count > 0)
            {
                CheckBoxList4.DataSource = null;
                CheckBoxList4.DataBind();
            }
            else
            {
                List<TestClass> myList = new List<TestClass>();
                myList.Add(new TestClass("value1", "数据绑定值 1"));
                myList.Add(new TestClass("value2", "数据绑定值 2"));
                myList.Add(new TestClass("value3", "数据绑定值 3"));
                myList.Add(new TestClass("value4", "数据绑定值 4"));
                myList.Add(new TestClass("value5", "数据绑定值 5"));
                myList.Add(new TestClass("value6", "数据绑定值 6"));


                CheckBoxList4.DataTextField = "Name";
                CheckBoxList4.DataValueField = "Id";
                CheckBoxList4.DataSource = myList;
                CheckBoxList4.DataBind();

                CheckBoxList4.SelectedValueArray = new string[] { "value1", "value2" };
            }
        }


    }
}