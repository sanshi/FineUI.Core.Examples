using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using FineUI.Core.Examples.Pages.DataModel.Models;

namespace FineUI.Core.Examples.Pages.DataModel
{

    public partial class FormHiddenPropertyModel : BaseModel
    {
        [BindProperty]
        public Student Student { get; set; }


        [HiddenProperty]
        public int UserRoleId { get; set; }
        [HiddenProperty]
        public string UserRoleName { get; set; }


        public void OnGet()
        {
            var model = new Student
            {
                Id = 101,
                Name = "张萍萍",
                Gender = 0,
                EntranceYear = 2000,
                AtSchool = true,
                Major = "材料科学与工程系",
                Group = 1,
                EntranceDate = DateTime.Parse("2000-09-01")
            };

            Student = model;

            // 页面初始化时赋值
            UserRoleId = 101;
            UserRoleName = "系统管理员";
        }

        public void btnModifyHiddenProperty_Click(object sender, EventArgs e)
        {
            // 本次回发不需要验证模型的有效性
            // https://fineui.com/docs/#/Questions/2000_modelstate
            ModelState.Clear();

            if (UserRoleName == "系统管理员")
            {
                UserRoleId = 202;
                UserRoleName = "超级管理员";
            }
            else
            {
                UserRoleId = 101;
                UserRoleName = "系统管理员";
            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // 在任意的后台事件处理函数中，可以轻松访问隐藏属性的值
            ShowNotify(new RawHtml("隐藏属性：<br/>UserRoleId：{0}<br/>UserRoleName：{1}", UserRoleId, UserRoleName));

        }

    }
}