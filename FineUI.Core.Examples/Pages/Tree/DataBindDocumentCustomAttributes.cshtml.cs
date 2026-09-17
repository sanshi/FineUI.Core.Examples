using Newtonsoft.Json.Linq;
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
    public partial class DataBindDocumentCustomAttributesModel : BaseModel
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
            string xmlPath = FineUI.Core.PageContext.MapWebPath("~/content/tree/website.xml");

            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xmlContent);

            ResolveXmlNodeList(Tree1.Nodes, xdoc.DocumentElement.ChildNodes);
        }

        private void ResolveXmlNodeList(IList<TreeNode> nodes, XmlNodeList xmlNodes)
        {
            foreach (XmlNode xmlNode in xmlNodes)
            {
                if (xmlNode.NodeType == XmlNodeType.Element)
                {
                    TreeNode node = new TreeNode();
                    nodes.Add(node);


                    foreach (XmlAttribute attribute in xmlNode.Attributes)
                    {
                        string name = attribute.Name;
                        string value = attribute.Value;

                        if (name == "Highlight")
                        {
                            node.Attributes["data-highlight"] = true;
                        }
                        else
                        {
                            node.SetPropertyValue(name, value);
                        }
                    }

                    if (xmlNode.ChildNodes.Count > 0)
                    {
                        ResolveXmlNodeList(node.Nodes, xmlNode.ChildNodes);
                    }
                }
            }
        }

        protected void Tree1_NodeClick(object sender, TreeNodeEventArgs e)
        {
            labResult.Text = String.Format("你点击了树节点：{1}（{0}）{2}",
                e.NodeID,
                e.Node.Text,
                e.Node.Attributes["data-highlight"] != null ? " - 高亮显示" : "");
        }
    }
}