using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FineUI.Core.Examples.Pages.FormTable
{
    public partial class TableStyleFileUploadModel : BaseUploadModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string originalName = filePhoto.ShortFileName;

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

                labResult.Text = "<p>文件路径：" + HtmlEncode(originalName) + "</p>" +
                    "<p>用户名：" + tbxUserName.Text + "</p>" +
                    "<p>头像：<br /><img src=\"" + GetImageUrl(savedName) + "\" /></p>";

                // 清空表单字段（清空上传控件，否则提交表单时会再次上传！）
                SimpleForm1.Reset();
            }
        }

    }
}