using FineUI.Core.Examples.Pages.DataModel.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.DataModel
{
    /// <summary>
    /// 数据模型编辑实体。
    ///
    /// <para>
    /// 回发时 [BindProperty] 字段是模型绑定器新建的<b>空壳</b>，只承载页面里 For 声明过、且客户端回传得来的
    /// 那几个值，与数据源没有任何关系。所以保存不能直接拿它整对象写回（没进表单的属性都是默认值，
    /// 会把已有数据清零），而要<b>按主键重新读出实体、只覆盖表单里出现过的字段</b>。
    /// </para>
    /// </summary>
    public partial class StudentEditModel : BaseModel
    {
        /// <summary>
        /// 表单绑定根：首屏由 OnGet 加载供回显；回发时模型绑定器新建空壳承载表单值。
        /// </summary>
        [BindProperty]
        public Student Student { get; set; }

        /// <summary>
        /// 首屏渲染前执行（回发不执行）：读 ?id 加载实体，供页面上的 For 回显。
        /// </summary>
        public IActionResult OnGet(int id)
        {
            Student = StudentStore.Find(id);
            if (Student == null)
            {
                // 中止本次请求：不渲染本页，改渲染框架内置极简页并弹出提示。
                // 本页常被窗体以 IFrame 弹出，故让用户点「确定」后顺手关掉父级窗体。
                return UIHelper.AbortPage("该学生不存在或已被删除！", ActiveWindow.GetHideReference());
            }

            // 真实项目在此判断当前用户能不能编辑这条记录，无权则同样 return UIHelper.AbortPage(...)。
            // ⚠️ 注意本方法只在首屏执行——回发时不跑，所以鉴权在事件处理器里还要再做一次（见 btnSave_Click）。
            // 本示例数据无归属关系，故只留说明。
            return Page();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // 按主键重新读出完整实体，只覆盖表单里出现过的字段；
            // 未列出的属性（分组 / 状态 / 爱好 / 家庭信息 / 成绩）保持原值。
            // 这份清单必须与页面里的 For 保持一致——往表单加字段而忘了补赋值，该字段就永远保存不上。
            // 它同时是 over-posting 的防线：绑定器会把构造 POST 里任何 Student.* 字段灌进这个空壳，
            // 但只有这里显式抄过去的字段才会落库。
            // 主键由页面上的隐藏字段随回发带回，客户端可以篡改成任意值——回发路径没有 OnGet，
            // 所以「当前用户能不能编辑这条记录」必须在这里再判一次（真实项目：无权则 ShowNotify + return）。
            var stored = StudentStore.Find(Student.Id);
            if (stored == null)
            {
                ShowNotify("该学生不存在或已被删除！", MessageBoxIcon.Error);
                return;
            }

            stored.Name = Student.Name;
            stored.Gender = Student.Gender;
            stored.EntranceYear = Student.EntranceYear;
            stored.AtSchool = Student.AtSchool;
            stored.Major = Student.Major;
            stored.EntranceDate = Student.EntranceDate;

            StudentStore.Update(stored);
            ShowNotify(String.Format("保存成功！ID={0}，姓名={1}", stored.Id, stored.Name), MessageBoxIcon.Success);
        }
    }
}
