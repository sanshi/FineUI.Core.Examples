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
    public partial class RadioButtonListUpdateModel : BaseModel
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


            RadioButtonList2.DataTextField = "Name";
            RadioButtonList2.DataValueField = "Id";
            RadioButtonList2.DataSource = myList;
            RadioButtonList2.DataBind();

            RadioButtonList2.SelectedValue = "value2";
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
            ShowNotify(String.Format("列表一的选中项：{0}", RadioButtonList1.SelectedValue));
        }

        protected void btnCheckedItemsList2_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表二的选中项：{0}", RadioButtonList2.SelectedValue));
        }

        protected void btnCheckedItemsList3_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表三的选中项：{0}", RadioButtonList3.SelectedValue));
        }

        protected void btnCheckedItemsList4_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表四的选中项：{0}", RadioButtonList4.SelectedValue));
        }


        protected void btnUpdateList1_Click(object sender, EventArgs e)
        {
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("value1", "数据绑定值 1"));
            myList.Add(new TestClass("value2", "数据绑定值 2"));
            myList.Add(new TestClass("value3", "数据绑定值 3"));
            myList.Add(new TestClass("value4", "数据绑定值 4"));

            RadioButtonList1.DataTextField = "Name";
            RadioButtonList1.DataValueField = "Id";
            RadioButtonList1.DataSource = myList;
            RadioButtonList1.DataBind();

            RadioButtonList1.SelectedValue = "value1";
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


            RadioButtonList2.DataTextField = "Name";
            RadioButtonList2.DataValueField = "Id";
            RadioButtonList2.DataSource = myList;
            RadioButtonList2.DataBind();

            RadioButtonList2.SelectedValue = "value1";
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

            RadioButtonList3.DataTextField = "Name";
            RadioButtonList3.DataValueField = "Id";
            RadioButtonList3.DataSource = myList;
            RadioButtonList3.DataBind();

            RadioButtonList3.SelectedValue = "value3";
        }

        protected void btnUpdateList4_Click(object sender, EventArgs e)
        {
            if (RadioButtonList4.Items.Count > 0)
            {
                RadioButtonList4.DataSource = null;
                RadioButtonList4.DataBind();
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


                RadioButtonList4.DataTextField = "Name";
                RadioButtonList4.DataValueField = "Id";
                RadioButtonList4.DataSource = myList;
                RadioButtonList4.DataBind();

                RadioButtonList4.SelectedValue = "value2";
            }
        }

    }
}