using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FileUploadToolbarModel : BaseUploadModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                // 校验：扩展名白名单 + 文件大小
                if (!ValidateUploadFile(filePhoto, out string error))
                {
                    // 清空上传控件（清空上传控件，否则提交表单时会再次上传！）
                    filePhoto.Reset();

                    ShowNotify(error);
                    return;
                }

                // 保存到 wwwroot 之外的目录，图片地址指向公共下载页（/Home/Download，带 inline=1 内联显示）
                string savedName = SaveUploadFile(filePhoto);
                imgPhoto.ImageUrl = GetImageUrl(savedName);

                // 清空上传控件（清空上传控件，否则提交表单时会再次上传！）
                filePhoto.Reset();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (imgPhoto.ImageUrl.EndsWith("blank.png"))
            {
                filePhoto.MarkInvalid("请先上传个人头像！");

                ShowNotify("请先上传个人头像！");

                return;
            }

            labResult.Text = "用户名：" + tbxUserName.Text + "<br/>" +
                    "邮箱：" + tbxEmail.Text + "<br/>" +
                    "<p>头像：<br /><img src=\"" + imgPhoto.ImageUrl + "\" /></p>";

            // 清空表单字段（清空上传控件，否则提交表单时会再次上传！）
            imgPhoto.ImageUrl = "~/res/images/blank.png";
            filePhoto.Reset();
            tbxEmail.Reset();
            tbxUserName.Reset();
        }

    }
}