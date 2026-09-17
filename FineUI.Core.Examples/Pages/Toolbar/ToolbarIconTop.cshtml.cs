using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class ToolbarIconTopModel : BaseUploadModel
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

    }
}