using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FileUploadDragDropModel : UploadPageBase
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labPhotoResult.Text = "单张图片：" + FormatUploadedFiles(filePhoto.PostedFiles);
            labPhotosResult.Text = "多张图片：" + FormatUploadedFiles(filePhotos.PostedFiles);
            SimpleForm1.Reset();
        }

        private static string FormatUploadedFiles(IEnumerable<IFormFile> files)
        {
            var results = new List<string>();
            foreach (var file in files)
            {
                results.Add(ValidateUploadFile(file, out string error) ? file.FileName : $"{file.FileName}（{error}）");
            }

            return results.Count == 0 ? "未选择文件" : string.Join("、", results);
        }
    }
}
