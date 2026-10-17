using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Mobile
{
    public partial class MainModel : BaseMobileModel
    {
        private static readonly JsonSerializerSettings ScriptDataSettings = new JsonSerializerSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeHtml
        };

        /// <summary>
        /// 首屏菜单的 JSON 文本，由 Page_Get 从菜单 XML 生成。默认空数组；序列化时转义菜单文字，
        /// 避免其中的 &lt;/script&gt; 提前结束页面上的非执行 JSON 数据块。
        /// </summary>
        public string MenuSourceJson { get; private set; } = "[]";

        public void Page_Get(object sender, EventArgs e)
        {
            // 页面模板会直接读取 MenuSourceJson，先组装菜单，再序列化成可安全输出的数据。
            XmlDocument doc = DataSourceUtil.LoadXml("~/wwwroot/res/menu.xml");
            XmlNode mobileRootNode = doc.SelectSingleNode("/Tree/TreeNode[@Text=\"移动\"]");

            JArray menuSource = new JArray();
            // 一级菜单跳过「移动首页」本身，只保留用户可以打开的页面。
            foreach (XmlNode node in mobileRootNode.ChildNodes)
            {
                string menuText = node.Attributes["Text"].Value;

                if (menuText == "移动首页")
                {
                    continue;
                }

                JObject menu = new JObject();
                menu.Add("text", menuText);

                ResolveXmlNode(node, menu);

                menuSource.Add(menu);
            }

            MenuSourceJson = JsonConvert.SerializeObject(menuSource, ScriptDataSettings);
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
