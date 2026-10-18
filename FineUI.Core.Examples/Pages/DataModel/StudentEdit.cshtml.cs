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
    public partial class StudentEditModel : PageBase
    {
        /// <summary>
        /// 表单绑定根：首屏填入数据库值；回发时保留用户提交的表单值。
        /// </summary>
        [BindProperty]
        public Student Student { get; set; }

        private Student _storedStudent;

        /// <summary>
        /// 每次按当前 URL 查找编辑目标；首屏才把数据库值填入表单。
        /// </summary>
        public void Page_Init(object sender, EventArgs e)
        {
            var id = GetQueryIntValue("id");
            _storedStudent = StudentStore.Find(id);
            if (_storedStudent == null)
            {
                // 中止本次请求：不渲染本页，改渲染框架内置极简页并弹出提示。
                // 本页常被窗体以 IFrame 弹出，故让用户点「确定」后顺手关掉父级窗体。
                FineUI.Core.PageContext.AbortPage("该学生不存在或已被删除！", ActiveWindow.HideCommand());
                return;
            }

            // 真实项目还应按本次 URL 和当前身份检查编辑权限。
            if (!IsPostBack)
            {
                Student = _storedStudent;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // 修改 Page_Init 已按本次 URL 查到的实体，只覆盖表单里出现过的字段；
            // 未列出的属性（分组 / 状态 / 爱好 / 家庭信息 / 成绩）保持原值。
            // 这份清单必须与页面里的 For 保持一致——往表单加字段而忘了补赋值，该字段就永远保存不上。
            // 它同时是 over-posting 的防线：绑定器会把构造 POST 里任何 Student.* 字段灌进这个空壳，
            // 但只有这里显式抄过去的字段才会落库。
            // 表单中的 Student.Id 可由客户端修改，不能据此决定更新哪条记录。
            _storedStudent.Name = Student.Name;
            _storedStudent.Gender = Student.Gender;
            _storedStudent.EntranceYear = Student.EntranceYear;
            _storedStudent.AtSchool = Student.AtSchool;
            _storedStudent.Major = Student.Major;
            _storedStudent.EntranceDate = Student.EntranceDate;

            StudentStore.Update(_storedStudent);
            ShowNotify(String.Format("保存成功！ID={0}，姓名={1}", _storedStudent.Id, _storedStudent.Name), MessageBoxIcon.Success);
        }
    }
}
