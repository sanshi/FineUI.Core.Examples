using System;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FileUploadDragDropAutoModel : UploadPageBase
    {
        protected void filePhotos_FileSelected(object sender, EventArgs e)
        {
            var files = filePhotos.PostedFiles;
            // 先校验整批。例如第二张图片超过 2 MB 时保留两张图片，让用户重新选择。
            foreach (var file in files)
            {
                if (!ValidateUploadFile(file, out string error))
                {
                    filePhotos.MarkInvalid($"{file.FileName}（{error}）");
                    return;
                }
            }

            labResult.Text = "服务器收到：" + string.Join("、", files.Select(file => file.FileName));
            filePhotos.Reset();
        }
    }
}
