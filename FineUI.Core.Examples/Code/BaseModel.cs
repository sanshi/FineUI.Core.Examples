using System.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples
{
    public class BaseModel : PageModel
    {

        #region IsPostBack

        /// <summary>
        /// 是否页面回发
        /// </summary>
        public bool IsPostBack
        {
            get
            {
                return FineUI.Core.PageContext.IsFineUIAjaxPostBack();
            }
        }

        #endregion

        #region RegisterStartupScript

        /// <summary>
        /// 注册客户端脚本
        /// </summary>
        /// <param name="scripts"></param>
        public void RegisterStartupScript(string scripts)
        {
            FineUI.Core.PageContext.RegisterStartupScript(scripts);
        }

        #endregion

        #region ViewBag

        private DynamicViewData _viewBag;

        /// <summary>
        /// 给 PageModel 加 ViewBag
        /// https://forums.asp.net/t/2128012.aspx?Razor+Pages+ViewBag+has+gone+
        /// https://github.com/aspnet/Mvc/issues/6754
        /// </summary>
        public dynamic ViewBag
        {
            get
            {
                if (_viewBag == null)
                {
                    _viewBag = new DynamicViewData(ViewData);
                }
                return _viewBag;
            }
        }
        #endregion

        #region 实用函数

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="fields"></param>
        public virtual void ShowNotify(IFormCollection values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("表单字段值：");
            sb.Append("<ul class=\"result\">");
            foreach (string key in values.Keys)
            {
                if (key == "__RequestVerificationToken" || key == "__EVENTTARGET" || key == "__EVENTARGUMENT" || key == "__FSTATE")
                {
                    continue;
                }
                sb.AppendFormat("<li>{0}: {1}</li>", key, values[key]);
            }
            sb.Append("</ul>");

            ShowNotify(new RawHtml(sb.ToString()));
        }

        // 桌面端通知：构建一个统一配置的 Notify（消息由调用方分别按普通文本 / 可信 HTML 设置）
        private Notify CreateNotify(MessageBoxIcon messageIcon, Target target)
        {
            Notify n = new Notify();
            n.Target = target;
            n.MessageBoxIcon = messageIcon;
            n.PositionX = Position.Center;
            n.PositionY = Position.Top;
            n.DisplayMilliseconds = 3000;
            n.ShowHeader = false;
            return n;
        }

        /// <summary>
        /// 显示通知对话框（普通文本，HTML 转义）
        /// </summary>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示通知对话框（普通文本，HTML 转义）
        /// </summary>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            ShowNotify(message, messageIcon, Target.Top);
        }

        /// <summary>
        /// 显示通知对话框（普通文本，HTML 转义）
        /// </summary>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon, Target target)
        {
            Notify n = CreateNotify(messageIcon, target);
            n.Message = message;
            n.Show();
        }

        /// <summary>
        /// 显示通知对话框（可信 HTML，原样输出不转义）
        /// </summary>
        public virtual void ShowNotify(RawHtml message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示通知对话框（可信 HTML，原样输出不转义）
        /// </summary>
        public virtual void ShowNotify(RawHtml message, MessageBoxIcon messageIcon)
        {
            ShowNotify(message, messageIcon, Target.Top);
        }

        /// <summary>
        /// 显示通知对话框（可信 HTML，原样输出不转义）
        /// </summary>
        public virtual void ShowNotify(RawHtml message, MessageBoxIcon messageIcon, Target target)
        {
            Notify n = CreateNotify(messageIcon, target);
            n.MessageRawHtml = message;
            n.Show();
        }

        /// <summary>
        /// 获取网址的完整路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetAbsoluteUrl(string virtualPath)
        {
            // http://benjii.me/2015/05/get-the-absolute-uri-from-asp-net-mvc-content-or-action/
            var urlBuilder = new System.UriBuilder(Request.GetDisplayUrl())
            {
                Path = Url.Content(virtualPath),
                Query = null,
            };

            return urlBuilder.ToString();
        }

        #endregion

        #region ShowNotify

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="form"></param>
        public void ShowNotify(params ControlBase[] controls)
        {
            List<Field> resolvedFields = new List<Field>();
            foreach (ControlBase control in controls)
            {
                ResolveFormFields(control, resolvedFields);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("表单字段值：");
            sb.Append("<ul class=\"result\">");
            foreach (Field field in resolvedFields)
            {
                string text = "";
                if (field is CheckBoxList)
                {
                    text = String.Join(", ", (field as CheckBoxList).SelectedValueArray);
                }
                else if (field is RadioButtonList)
                {
                    text = (field as RadioButtonList).SelectedValue;
                }
                else
                {
                    // 获取 Text 属性的值
                    var propertyInfo = field.GetType().GetProperty("Text", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (propertyInfo != null)
                    {
                        text = propertyInfo.GetValue(field).ToString();
                    }
                }

                if (!String.IsNullOrEmpty(text))
                {
                    sb.AppendFormat("<li>{0}: {1}</li>", field.ID, text);
                }
            }
            sb.Append("</ul>");

            ShowNotify(new RawHtml(sb.ToString()));
        }

        private void ResolveFormFields(ControlBase control, List<Field> resolvedFields)
        {
            if (control is PanelBase)
            {
                if (control is Form)
                {
                    foreach (FormRow row in (control as Form).Rows)
                    {
                        foreach (ControlBase rowItem in row.Items)
                        {
                            ResolveFormFields(rowItem, resolvedFields);
                        }
                    }
                }

                foreach (ControlBase item in (control as PanelBase).Items)
                {
                    ResolveFormFields(item, resolvedFields);
                }
            }
            else if (control is Field)
            {
                resolvedFields.Add(control as Field);
            }
        }

        #endregion

        #region HtmlEncode / EncodeJson

        /// <summary>
        /// HTML 编码：把用户输入/可控数据安全地输出到页面，防止 XSS。
        /// </summary>
        protected string HtmlEncode(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        /// <summary>
        /// 将 JSON 数据（如 Grid 的 ModifiedData/FilteredData/MergedData）格式化为缩进字符串并 HTML 编码，
        /// 安全输出到页面。处理表格改动时先保存 GetModifiedData() 的结果，后续遍历和输出都使用这份快照。
        /// </summary>
        protected string EncodeJson(Newtonsoft.Json.Linq.JToken data)
        {
            return HtmlEncode(data == null ? "" : data.ToString(Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        /// 将任意对象序列化为缩进 JSON 字符串并 HTML 编码，安全输出到页面。
        /// 典型用法：String.Format("用户提交的数据：<pre>{0}</pre>", EncodeJson(TheModel))
        /// </summary>
        protected string EncodeJson(object data)
        {
            return HtmlEncode(Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
        }

        #endregion

        #region override - OnPageHandlerExecuting

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            if (IsPostBack)
            {
                // 回发请求时，检索请求数据或者Cookie，来验证当前访问是否有效。请求无效时，弹出错误提示，不再执行Page_Load和回发事件。
                if (!String.IsNullOrEmpty(Request.Query["error"]))
                {
                    ShowNotify("身份验证失败！");

                    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.pagehandlerexecutingcontext?view=aspnetcore-8.0
                    // 设置context.Result=UIHelper.Result()，可以中断页面继续执行（跳过接下来的 Page_Load 和回发事件）。
                    context.Result = UIHelper.Result();
                }
            }
        }

        /// <summary>
        /// 把一个单元格的新值写进数据行（单元格编辑保存时使用）
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="rowDict">本行改动过的单元格（键是列名）</param>
        /// <param name="rowData">要更新的数据行</param>
        protected void UpdateDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                object value = rowDict[columnName];

                // 客户端把清空的单元格发成空字符串；列不是字符串类型时直接写会抛异常，存 DBNull 表示空值
                if ("".Equals(value) && rowData.Table.Columns[columnName].DataType != typeof(string))
                {
                    rowData[columnName] = DBNull.Value;
                }
                else
                {
                    rowData[columnName] = value;
                }
            }
        }

        /// <summary>
        /// 按行标识在数据表里找到对应的数据行，找不到返回 null
        /// </summary>
        protected DataRow FindRowByID(DataTable table, int rowId)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["Id"]) == rowId)
                {
                    return row;
                }
            }
            return null;
        }

        /// <summary>
        /// 按行标识删除数据表里的一行（找不到就什么也不做）
        /// </summary>
        protected void DeleteRowByID(DataTable table, int rowID)
        {
            DataRow found = FindRowByID(table, rowID);
            if (found != null)
            {
                table.Rows.Remove(found);
            }
        }

        /// <summary>
        /// 取下一个可用的行标识（模拟数据库的自增长列：当前最大值 + 1）
        /// </summary>
        protected int GetNextRowID(DataTable table)
        {
            int maxID = 0;
            foreach (DataRow row in table.Rows)
            {
                int currentRowID = Convert.ToInt32(row["Id"]);
                if (currentRowID > maxID)
                {
                    maxID = currentRowID;
                }
            }
            return maxID + 1;
        }

        #endregion
    }
}
