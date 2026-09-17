using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class MenuCheckBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateLangResult();
                UpdateSiteResult();
            }
        }

        private void UpdateLangResult()
        {
            string selectedLangName = String.Empty;
            foreach (MenuItem item in btnLangMenu.Menu.Items)
            {
                if (item is MenuCheckBox && (item as MenuCheckBox).Checked)
                {
                    selectedLangName = item.Text;
                    break;
                }
            }
            labLangResult.Text = "你选择的语言：" + selectedLangName;
        }

        private void UpdateSiteResult()
        {
            StringBuilder selectedSites = new StringBuilder();
            foreach (MenuItem item in btnSiteMenu.Menu.Items)
            {
                if (item is MenuCheckBox && (item as MenuCheckBox).Checked)
                {
                    selectedSites.AppendFormat("{0}, ", item.Text);
                }
            }
            labSiteResult.Text = "你选择的站点：" + selectedSites.ToString().TrimEnd(", ".ToCharArray());
        }

        protected void MenuLang_CheckedChanged(object sender, CheckedEventArgs e)
        {
            UpdateLangResult();
        }

        protected void MenuSite_CheckedChanged(object sender, CheckedEventArgs e)
        {
            UpdateSiteResult();
        }


    }
}