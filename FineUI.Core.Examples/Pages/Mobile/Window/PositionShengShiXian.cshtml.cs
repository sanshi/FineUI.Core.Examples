using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Mobile.Window
{
    public partial class PositionShengShiXianModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSheng();
                BindShi();
                BindXian();
            }
        }


        private void BindSheng()
        {
            ddlSheng.DataSource = DataSourceUtil.SHENG_JSON;
            ddlSheng.DataBind();

            ddlSheng.Items.Insert(0, new ListItem("选择省份", "-1"));
            ddlSheng.SelectedValue = "-1";
        }

        private void BindShi()
        {
            string sheng = ddlSheng.SelectedValue;

            if (!String.IsNullOrEmpty(sheng) && sheng != "-1")
            {
                JArray ja = DataSourceUtil.SHI_JSON.Value<JArray>(sheng);
                ddlShi.DataSource = ja;
                ddlShi.DataBind();
            }

            ddlShi.Items.Insert(0, new ListItem("选择地区市", "-1"));
            ddlShi.SelectedValue = "-1";

            // 是否禁用
            ddlShi.Enabled = !(ddlShi.Items.Count == 1);
        }

        private void BindXian()
        {
            string shi = ddlShi.SelectedValue;

            if (!String.IsNullOrEmpty(shi) && shi != "-1")
            {
                JArray ja = DataSourceUtil.XIAN_JSON.Value<JArray>(shi);
                ddlXian.DataSource = ja;
                ddlXian.DataBind();
            }

            ddlXian.Items.Insert(0, new ListItem("选择县级市", "-1"));
            ddlXian.SelectedValue = "-1";

            // 是否禁用
            ddlXian.Enabled = !(ddlXian.Items.Count == 1);
        }

        protected void ddlSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlShi.Items.Clear();
            BindShi();

            ddlXian.Items.Clear();
            BindXian();
        }

        protected void ddlShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlXian.Items.Clear();
            BindXian();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            // 隐藏窗体
            Window1.Hidden = true;

            // 弹出选中的值
            ShowNotify("您选择的省市县：" + ddlSheng.SelectedValue + " | " + ddlShi.SelectedValue + (ddlXian.SelectedValue == "-1" ? "" : " | " + ddlXian.SelectedValue));

        }

    }
}