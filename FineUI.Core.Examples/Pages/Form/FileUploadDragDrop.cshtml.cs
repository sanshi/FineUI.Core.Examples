using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FileUploadDragDropModel : UploadPageBase
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var results = new List<string>();
            foreach (var file in filePhoto.PostedFiles.Concat(filePhotos.PostedFiles))
            {
                results.Add(ValidateUploadFile(file, out string error) ? file.FileName : $"{file.FileName}（{error}）");
            }

            string message = results.Count == 0 ? "请选择图片。" : "服务器收到：" + string.Join("、", results);
            labResult.Text = message;
            SimpleForm1.Reset();
        }
    }
}
