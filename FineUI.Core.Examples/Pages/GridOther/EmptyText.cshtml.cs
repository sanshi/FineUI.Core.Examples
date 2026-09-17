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
    public partial class EmptyTextModel : BaseModel
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
            var sourceKey = Grid1.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "source2")
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();

                sourceKey = "source1";
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();

                sourceKey = "source2";
            }

            Grid1.Attributes["data-source-key"] = sourceKey;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}