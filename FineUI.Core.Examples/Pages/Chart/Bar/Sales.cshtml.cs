using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class SalesModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSales(false);
                BindOrders();
            }
        }

        private void BindSales(bool recent)
        {
            ChartSales.DataSource = SalesOrderData.Months(recent);
            ChartSales.DataBind();
        }

        private void BindOrders(string month = null, bool recent = false)
        {
            var orders = SalesOrderData.Details(month, recent);
            GridOrders.DataSource = orders;
            GridOrders.DataBind();
            ToolbarTextOrderScope.Text = SalesOrderData.OrderScope(month, recent, orders.Length);
        }

        protected void ChartSales_PointClick(object sender, ChartPointEventArgs e)
        {
            // 月份来自浏览器，例如 2026-09。只查询属于示例订单范围的月份。
            if (SalesOrderData.HasMonth(e.PointID))
            {
                BindOrders(e.PointID);
            }
        }

        protected void btnRecent_Click(object sender, EventArgs e)
        {
            BindSales(true);
            BindOrders(recent: true);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindSales(false);
            BindOrders();
        }
    }
}
