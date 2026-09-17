using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class WebUploaderCancelModel : BaseWebUploaderModel
    {
        private static readonly string KEY_FOR_DATASOURCE_SESSION = "webuploader.webuploader_cancel";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "RebindGrid")
            {
                LoadData();
            }
            else if (e.EventName == "DeleteRow")
            {
                DeleteRow(KEY_FOR_DATASOURCE_SESSION, e.EventArguments);
                LoadData();
            }
            else if (e.EventName == "DeleteRows")
            {
                foreach (string rowId in e.EventArgumentsAsJArray)
                {
                    DeleteRow(KEY_FOR_DATASOURCE_SESSION, rowId);
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            Grid1.DataSource = GetSourceData(KEY_FOR_DATASOURCE_SESSION);
            Grid1.DataBind();
        }

    }
}
