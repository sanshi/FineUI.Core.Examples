using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace FineUI.Core.Examples.Pages
{
    public partial class IndexModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        #region LoadData

        // 框架页风格
        private string _framePageStyle = "f-dark-left";
        // 是否仅显示社区版示例
        private bool _showOnlyCommunity = false;
        // 显示模式
        private string _displayMode = "normal";
        // 主选项卡标签
        private string _mainTabs = "multi";
        // 语言
        private string _lang = "zh_CN";
        // 搜索文本
        private string _searchText = "";
        // 示例数
        private int _examplesCount = 0;

        private void LoadData()
        {
            string cookie = String.Empty;

            // 从Cookie中读取 - 框架页风格
            cookie = Request.Cookies["FramePageStyle"];
            if (cookie != null)
            {
                _framePageStyle = cookie;
            }

            // 从Cookie中读取 - 是否仅显示社区版示例
            cookie = Request.Cookies["ShowOnlyCommunity"];
            if (cookie != null)
            {
                _showOnlyCommunity = Convert.ToBoolean(cookie);
            }

            // 从Cookie中读取 - 显示模式
            cookie = Request.Cookies["DisplayMode"];
            if (!String.IsNullOrEmpty(cookie))
            {
                _displayMode = cookie;
            }

            // 从Cookie中读取 - 语言
            cookie = Request.Cookies["Language"];
            if (!String.IsNullOrEmpty(cookie))
            {
                _lang = cookie;
            }

            // 从Cookie中读取 - 搜索文本
            cookie = Request.Cookies["SearchText"];
            if (!String.IsNullOrEmpty(cookie))
            {
                _searchText = HttpUtility.UrlDecode(cookie);
            }

            // 从Cookie中读取 - 主选项卡标签
            cookie = Request.Cookies["MainTabs"];
            if (!String.IsNullOrEmpty(cookie))
            {
                _mainTabs = cookie;
            }


            // 初始化设置 - 框架页风格
            SetCheckedMenuItem(MenuFramePageStyle, _framePageStyle);

            // 初始化设置 - 显示模式
            SetCheckedMenuItem(MenuDisplayMode, _displayMode);

            // 初始化设置 - 主选项卡标签
            SetCheckedMenuItem(MenuMainTabs, _mainTabs);

            // 初始化设置 - 语言
            SetCheckedMenuItem(MenuLang, _lang);


            // 初始化搜索文本
            if (!String.IsNullOrEmpty(_searchText))
            {
                ttbxSearch.Text = _searchText;
                ttbxSearch.ShowTrigger1 = true;
                ttbxSearch.Width = 200;
                ttbxSearch.CssClass = "searchbox expanded";
            }

            // 只显示社区版示例
            if(_showOnlyCommunity)
            {
                btnUserAvatar.Badge = true;
                btnUserAvatar.BadgeText = "仅社区版";
                btnUserAvatar.BadgeType = BadgeType.Warning;
            }


            InitTreeMenu();

            btnDownload.Badge = !Constants.IS_COMMUNITY_EDITION;
            cbxShowOnlyCommunity.Checked = _showOnlyCommunity;
            //mainTabStrip.ShowTabHeader = _mainTabs == "multi";
            hfExamplesCount.Text = _examplesCount.ToString();
        }

        #endregion

        #region InitTreeMenu

        private void InitTreeMenu()
        {
            string xmlPath = FineUI.Core.PageContext.MapWebPath("~/res/menu.xml");

            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);


            // 返回全部的叶子节点个数
            _examplesCount = ResolveXmlNodeList(treeMenu.Nodes, doc.DocumentElement.ChildNodes);

        }


        private int _nodeIndex = 0;

        private int ResolveXmlNodeList(IList<TreeNode> nodes, XmlNodeList xmlNodes)
        {
            // nodes 中渲染到页面上的节点个数
            int nodeVisibleCount = 0;

            foreach (XmlNode xmlNode in xmlNodes)
            {
                if (xmlNode.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                TreeNode node = new TreeNode();

                // 是否叶子节点
                bool isLeaf = xmlNode.ChildNodes.Count == 0;

                bool currentNodeIsVisible = true;

                string nodeText = "";
                bool nodeIsEnterprise = false;

                XmlAttribute textAttr = xmlNode.Attributes["Text"];
                if (textAttr != null)
                {
                    nodeText = textAttr.Value;
                }

                // 是否企业版
                XmlAttribute isEnterpriseAttr = xmlNode.Attributes["IsEnterprise"];
                if (isEnterpriseAttr != null)
                {
                    nodeIsEnterprise = isEnterpriseAttr.Value.ToLower() == "true";
                }

                string nodeVersion = "";
                // 示例关联的版本号
                XmlAttribute versionAttr = xmlNode.Attributes["Version"];
                if (versionAttr != null)
                {
                    nodeVersion = versionAttr.Value;
                }


                int childVisibleCount = 0;
                if (isLeaf)
                {
                    // 仅显示社区版示例
                    if (_showOnlyCommunity && nodeIsEnterprise)
                    {
                        currentNodeIsVisible = false;
                    }

                    // 存在搜索文本
                    if (!String.IsNullOrEmpty(_searchText))
                    {
                        if (!nodeText.Contains(_searchText))
                        {
                            currentNodeIsVisible = false;
                        }
                    }
                }
                else
                {
                    // 递归
                    childVisibleCount = ResolveXmlNodeList(node.Nodes, xmlNode.ChildNodes);

                    nodeVisibleCount += childVisibleCount;

                    if (childVisibleCount == 0)
                    {
                        currentNodeIsVisible = false;
                    }
                    else
                    {
                        // 存在搜索文本
                        if (!String.IsNullOrEmpty(_searchText))
                        {
                            // 展开节点
                            node.Expanded = true;
                        }
                    }

                    // 目录节点不可选择
                    node.Selectable = false;
                }

                if (currentNodeIsVisible)
                {
                    foreach (XmlAttribute attribute in xmlNode.Attributes)
                    {
                        string name = attribute.Name;
                        string value = attribute.Value;

                        if (name == "Text")
                        {
                            // Text需要特殊处理
                            if (isLeaf)
                            {
                                // 设置节点的提示信息
                                node.ToolTip = nodeText;
                            }

                            // 存在 IsEnterprise=True 属性，则改变 Text 的值
                            if (nodeIsEnterprise)
                            {
                                node.IconFont = IconFont._Enterprise;
                                //nodeText = nodeText + "&nbsp;<span class=\"iscorp\">Corp.</span>";
                            }

                            StringBuilder nodeTextBuilder = new StringBuilder();
                            nodeTextBuilder.AppendFormat("<span class=\"text\">{0}</span>", nodeText);

                            if (childVisibleCount > 0)
                            {
                                nodeTextBuilder.AppendFormat("<span class=\"menu-child-count\">{0}</span>", childVisibleCount);
                            }

                            if(!String.IsNullOrEmpty(nodeVersion))
                            {
                                nodeTextBuilder.AppendFormat("<span class=\"menu-version\">{0}</span>", nodeVersion);
                            }

                            // 节点文本是开发者拼接的可信 HTML，用 TextRawHtml 标记为原样输出（不转义）
                            node.TextRawHtml = new RawHtml(nodeTextBuilder.ToString());
                        }
                        else
                        {
                            node.SetPropertyValue(name, value);
                        }
                    }

                    // 为每个节点分配一个ID
                    node.NodeID = String.Format("tn_{0}", _nodeIndex++);

                    nodes.Add(node);



                    // 示例数只计算叶子节点
                    if (isLeaf)
                    {
                        nodeVisibleCount++;
                    }

                }

            }

            return nodeVisibleCount;
        }

        #endregion

        #region SetCheckedMenuItem

        private void SetCheckedMenuItem(MenuButton menuButton, string checkedValue)
        {
            foreach (MenuItem item in menuButton.Menu.Items)
            {
                MenuCheckBox checkBox = (item as MenuCheckBox);
                if (checkBox != null)
                {
                    checkBox.Checked = checkBox.AttributeDataTag == checkedValue;
                }
            }
        }

        #endregion
    }
}
