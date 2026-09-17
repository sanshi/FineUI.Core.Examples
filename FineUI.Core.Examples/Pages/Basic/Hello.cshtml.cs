using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Basic
{
    public partial class HelloModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnHello_Click(object sender, EventArgs e)
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);
        }

        
        protected void btnHello2_Click(object sender, EventArgs e)
        {
            Alert.ShowInTop("你好 FineUI！", MessageBoxIcon.Information);
        }
    }
}