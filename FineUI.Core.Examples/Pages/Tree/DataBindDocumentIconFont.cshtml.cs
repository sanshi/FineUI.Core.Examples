using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class DataBindDocumentIconFontModel : BaseModel
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
            Tree1.DataSource = GetDataSource();
            Tree1.DataBind();
        }

        private XmlDocument GetDataSource()
        {
            string xmlPath = FineUI.Core.PageContext.MapWebPath("~/content/tree/website_iconfont.xml");

            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xmlContent);

            return xdoc;
        }


    }
}