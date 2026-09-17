using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Editor
{
    public partial class CKEditorTabStripModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify(new RawHtml("编辑器一：{0}<br/>编辑器二：{1}", HttpUtility.HtmlEncode(HtmlEditor1.Text), HttpUtility.HtmlEncode(HtmlEditor2.Text)));
        }

    }
}