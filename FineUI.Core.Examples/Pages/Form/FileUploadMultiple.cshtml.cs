using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FileUploadMultipleModel : BaseUploadModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var msg = new List<string>();
            var skipped = new List<string>();

            foreach (IFormFile filePhoto in filePhotos.PostedFiles)
            {
                string originalName = filePhoto.FileName;

                // 校验：扩展名白名单 + 文件大小；不通过则跳过该文件
                if (!ValidateUploadFile(filePhoto, out string error))
                {
                    skipped.Add(HtmlEncode(originalName) + "（" + error + "）");
                    continue;
                }

                // 保存到 wwwroot 之外的目录，图片地址指向公共下载页（/Home/Download，带 inline=1 内联显示）
                string savedName = SaveUploadFile(filePhoto);

                msg.Add("<div>路径：" + HtmlEncode(originalName) + "</div>" +
                    "<div>照片：<br /><img src=\"" + GetImageUrl(savedName) + "\" /></div>");
            }

            labResult.Text = "<ol><li>" + string.Join("</li><li>", msg) + "</li></ol>";

            // 有被跳过的文件时给出提示
            if (skipped.Count > 0)
            {
                ShowNotify("已跳过 " + skipped.Count + " 个文件：" + string.Join("；", skipped));
            }

            // 清空表单字段（清空上传控件，否则提交表单时会再次上传！）
            SimpleForm1.Reset();
        }


    }
}
