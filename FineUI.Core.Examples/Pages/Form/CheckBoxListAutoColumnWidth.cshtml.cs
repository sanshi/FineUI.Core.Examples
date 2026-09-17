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
    public partial class CheckBoxListAutoColumnWidthModel : BaseModel
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


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm1);
        }

        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowNotify(String.Format("列表三的选中项：{0}", String.Join(", ", CheckBoxList3.SelectedValueArray)));
        }

        protected void btnServerSetSelectedValue_Click(object sender, EventArgs e)
        {
            CheckBoxList1.SelectedValueArray = new string[] { "value1", "value3" };
        }

        protected void btnServerGetSelectedValue_Click(object sender, EventArgs e)
        {
            if (CheckBoxList1.SelectedValueArray.Length > 0)
            {
                ShowNotify(String.Format("列表一的选中项：{0}", String.Join(", ", CheckBoxList1.SelectedValueArray)));
            }
            else
            {
                ShowNotify(String.Format("列表一没有选中项！"));
            }
        }

    }
}