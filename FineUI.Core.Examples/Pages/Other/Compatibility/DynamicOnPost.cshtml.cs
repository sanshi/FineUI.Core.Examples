using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class DynamicOnPostModel : PageBase
    {
        public void OnGet()
        {
            TextBox userName = new TextBox
            {
                ID = "tbxUserName",
                Label = "用户名",
                ShowRedStar = true,
                Required = true,
                EmptyText = "请输入用户名"
            };
            FineUI.Core.DropDownList gender = new FineUI.Core.DropDownList
            {
                ID = "ddlGender",
                Label = "性别（回发事件）",
                AutoSelectFirstItem = false
            };
            gender.Items.Add("男", "0");
            gender.Items.Add("女", "1");
            gender.Events.Add(new Event("change", Url.Handler("ddlGender_SelectedIndexChanged"), "ddlGender"));
            ViewBag.DynamicItems = new Field[] { userName, gender };
        }

        public IActionResult OnPostDdlGender_SelectedIndexChanged(string ddlGender_text)
        {
            ShowNotify("选择的性别：" + ddlGender_text);
            return UIHelper.Result();
        }

        public IActionResult OnPostButton1_Click(string tbxUserName, string ddlGender_text)
        {
            ShowNotify("用户名：" + tbxUserName + "  性别：" + ddlGender_text);
            return UIHelper.Result();
        }
    }
}
