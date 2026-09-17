using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeIconFontColorXmlModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        
        // 需要添加到页面上的CSS
        Dictionary<string, string> dynamicCssDic = new Dictionary<string, string>();

        private void LoadData()
        {
            string xmlPath = FineUI.Core.PageContext.MapWebPath("~/content/tree/tree_iconfont_randomcolor.xml");

            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xmlContent);
            ResolveXmlNodeList(Tree1.Nodes, xdoc.DocumentElement.ChildNodes);


            // 需要动态添加的CSS样式
            StringBuilder cssSB = new StringBuilder();
            foreach (string css in dynamicCssDic.Keys)
            {
                cssSB.AppendFormat("{0}", dynamicCssDic[css]);
            }
            // 注册CSS样式
            RegisterStartupScript(String.Format("F.addCSS('dynamicCSSStyles','{0}');", cssSB.ToString()));
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

                        if (name == "IconFontColor")
                        {
                            // 规范化后的 CSS 名称
                            string normalizedCssName = "tn_color_" + value.Replace('#', '_');
                            
                            // 动态添加 CSS
                            if (!dynamicCssDic.ContainsKey(normalizedCssName))
                            {
                                dynamicCssDic[normalizedCssName] = String.Format(".{0} .f-tree-folder,.{0} .f-tree-cell-text{{color:{1};}}", normalizedCssName, value);
                            }


                            node.CssClass = normalizedCssName;
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

        protected void btnGetSelectedNode_Click(object sender, EventArgs e)
        {
            string selectedID = Tree1.SelectedNodeID;
            if (!String.IsNullOrEmpty(selectedID))
            {
                labResult.Text = String.Format("选中的节点：{0}（{1}）", Tree1.FindNode(selectedID).Text, selectedID);
            }
            else
            {
                labResult.Text = "没有选中节点";
            }
        }

    }
}