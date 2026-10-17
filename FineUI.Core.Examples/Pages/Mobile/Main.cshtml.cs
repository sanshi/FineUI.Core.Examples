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
        /// 首屏的移动菜单数据。先在 Page_Get 中读取菜单 XML，再交给页面输出为 JSON。
        /// </summary>
        public JArray MenuSource { get; private set; } = new JArray();

        /// <summary>
        /// 给页面的非执行 JSON 数据块使用。转义菜单文本中的小于号等字符，避免 &lt;/script&gt; 提前结束标签。
        /// </summary>
        public string MenuSourceJson => JsonConvert.SerializeObject(MenuSource, ScriptDataSettings);

        public void Page_Get(object sender, EventArgs e)
        {
            // 页面中的 JSON 数据标签在 Razor 渲染时读取 MenuSourceJson，必须在渲染前准备菜单。
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
