using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class MenuDynamicModel : BaseModel
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
            XmlDocument doc = DataSourceUtil.LoadXml("~/wwwroot/content/toolbar/menu.xml");

            // 根节点（中国科学技术大学）
            XmlNode node = doc.DocumentElement.ChildNodes[0];
            FineUI.Core.Button btn = new FineUI.Core.Button();
            btn.Text = node.Attributes["text"].Value;
            Toolbar1.Items.Add(btn);

            ResolveMenu(btn, node.ChildNodes);
        }

        private void ResolveMenu(ControlBase btn, XmlNodeList nodes)
        {
            PropertyInfo menuInfo = btn.GetType().GetProperty("Menu");
            Menu menu = menuInfo.GetValue(btn, null) as Menu;

            foreach (XmlNode node in nodes)
            {
                XmlAttribute attrURL = node.Attributes["navigateurl"];
                if (attrURL != null)
                {
                    FineUI.Core.MenuHyperLink lnk = new FineUI.Core.MenuHyperLink();
                    lnk.Text = node.Attributes["text"].Value;
                    lnk.NavigateUrl = attrURL.Value;
                    lnk.Target = "_blank";

                    menu.Items.Add(lnk);

                    if (node.ChildNodes.Count > 0)
                    {
                        ResolveMenu(lnk, node.ChildNodes);
                    }
                }
            }
        }



        //private XmlDocument DataSourceUtil.LoadXml()
        //{
        //    // 加载XML配置文件
        //    string xmlPath = FineUI.Core.PageContext.MapWebPath("~/content/toolbar/menu.xml");
        //    string xmlContent = String.Empty;
        //    using (StreamReader sr = new StreamReader(xmlPath))
        //    {
        //        xmlContent = sr.ReadToEnd();
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xmlContent);

        //    return doc;
        //}


    }
}