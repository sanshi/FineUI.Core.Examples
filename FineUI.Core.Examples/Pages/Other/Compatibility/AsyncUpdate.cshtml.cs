using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class AsyncUpdateModel : PageBase
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostIncreaseAsync(int quantity)
        {
            // 模拟等待远程服务返回；真实项目可替换为数据库或远程业务调用。
            await Task.Delay(100, HttpContext.RequestAborted);
            int nextQuantity = System.Math.Clamp(quantity + 1, 0, 100);
            UIHelper.NumberBox("nbxQuantity").Text(nextQuantity.ToString());
            UIHelper.Label("labResult").Text($"当前数量：{nextQuantity}");
            return UIHelper.Result();
        }
    }
}
