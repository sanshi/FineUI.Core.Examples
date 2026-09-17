using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class WebUploaderMultiModel : BaseWebUploaderModel
    {
        private static readonly string KEY_FOR_DATASOURCE_1_SESSION = "webuploader.webuploader_multi.1";
        private static readonly string KEY_FOR_DATASOURCE_2_SESSION = "webuploader.webuploader_multi.2";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "RebindGrid_Grid1")
            {
                BindGrid1();
            }
            else if (e.EventName == "RebindGrid_Grid2")
            {
                BindGrid2();
            }
            else if (e.EventName == "DeleteRow_Grid1")
            {
                DeleteRow(KEY_FOR_DATASOURCE_1_SESSION, e.EventArguments);
                BindGrid1();
            }
            else if (e.EventName == "DeleteRow_Grid2")
            {
                DeleteRow(KEY_FOR_DATASOURCE_2_SESSION, e.EventArguments);
                BindGrid2();
            }
        }

        private void LoadData()
        {
            BindGrid1();

            BindGrid2();
        }

        private void BindGrid1()
        {
            Grid1.DataSource = GetSourceData(KEY_FOR_DATASOURCE_1_SESSION);
            Grid1.DataBind();
        }

        private void BindGrid2()
        {
            Grid2.DataSource = GetSourceData(KEY_FOR_DATASOURCE_2_SESSION);
            Grid2.DataBind();
        }

    }
}
