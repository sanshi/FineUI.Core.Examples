using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Mobile
{
    public partial class MainModel : BaseMobileModel
    {
        /// <summary>
        /// 首屏的移动菜单数据。模板输出为 JSON，客户端只解析数据，不拼接执行脚本。
        /// </summary>
        public JArray MenuSource { get; private set; } = new JArray();

        public void Page_Get(object sender, EventArgs e)
        {
            // 页面中的 JSON 数据标签在 Razor 渲染时读取 MenuSource，必须在渲染前准备菜单。
            // 控件字段在这里尚未绑定；只读取菜单 XML，回发不再次加载它。
            LoadData();
        }

        private void LoadData()
        {
            XmlDocument doc = DataSourceUtil.LoadXml("~/wwwroot/res/menu.xml");
            XmlNode mobileRootNode = doc.SelectSingleNode("/Tree/TreeNode[@Text=\"移动\"]");

            JArray menuSource = new JArray();
            // 一级菜单
            foreach (XmlNode node in mobileRootNode.ChildNodes)
            {
                string menuText = node.Attributes["Text"].Value;

                if (menuText == "移动首页")
                {
                    continue; // 不添加本项
                }

                JObject menu = new JObject();
                menu.Add("text", menuText);

                ResolveXmlNode(node, menu);

                menuSource.Add(menu);
            }


            MenuSource = menuSource;
        }

        private void ResolveXmlNode(XmlNode node, JObject parentMenu)
        {
            // 二级菜单
            JArray subMenus = new JArray();
            foreach (XmlNode subnode in node.ChildNodes)
            {
                JObject subMenu = new JObject();
                string subMenuText = subnode.Attributes["Text"].Value;

                if (subnode.HasChildNodes)
                {
                    subMenu.Add("text", subMenuText);

                    ResolveXmlNode(subnode, subMenu);
                }
                else
                {
                    string subMenuNavigateUrl = subnode.Attributes["NavigateUrl"].Value;

                    subMenu.Add("text", subMenuText);
                    subMenu.Add("navigateUrl", Url.Content(subMenuNavigateUrl.Replace("/?file=", "/")));
                }

                subMenus.Add(subMenu);
            }

            parentMenu.Add("children", subMenus);
        }
    }
}
