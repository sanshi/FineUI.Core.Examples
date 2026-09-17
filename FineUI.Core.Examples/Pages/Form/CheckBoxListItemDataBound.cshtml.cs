using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class CheckBoxListItemDataBoundModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 手工设置复选框列表项的提示信息
                CheckBoxList1.Items[0].Attributes["data-qtip"] = "这是第一个选项的提示信息";
                CheckBoxList1.Items[1].Attributes["data-qtip"] = "这是第二个选项的提示信息";
                CheckBoxList1.Items[2].Attributes["data-qtip"] = "这是第三个选项的提示信息";

                LoadData();
            }
        }

        
        private void LoadData()
        {
            List<TestClass> myList = new List<TestClass>();
            myList.Add(new TestClass("item1", "数据绑定值 1"));
            myList.Add(new TestClass("item2", "数据绑定值 2"));
            myList.Add(new TestClass("item3", "数据绑定值 3"));
            myList.Add(new TestClass("item4", "数据绑定值 4"));

            CheckBoxList2.DataTextField = "Name";
            CheckBoxList2.DataValueField = "Id";
            CheckBoxList2.DataSource = myList;
            CheckBoxList2.DataBind();

            CheckBoxList2.SelectedValueArray = new string[] { "item1", "item3" };
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

        #region Events


        /// <summary>
        /// 复选框列表项数据绑定时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBoxList2_ItemDataBound(object sender, CheckItemEventArgs e)
        {
            TestClass dataItem = e.DataItem as TestClass;

            e.Item.Attributes["data-qtip"] = String.Format("{0}（值：{1}）", dataItem.Name, dataItem.Id);
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm1);
        }

        #endregion

    }
}