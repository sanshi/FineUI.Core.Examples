using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class ModelBindingModel : PageBase
    {
        [BindProperty]
        public OrderInput Data { get; set; }

        public void OnGet(int id)
        {
            Data = new OrderInput { Name = $"订单{id}", Quantity = 2 };
        }

        public IActionResult OnPostSave(int id)
        {
            if (!ModelState.IsValid)
            {
                UIHelper.Label("labResult").Text("请填写名称和大于零的数量");
                // 页面已经展示校验提示，不再额外弹出默认的模型错误对话框。
                return UIHelper.Result(checkModelState: false);
            }

            // 实际保存前还需按当前用户校验该订单的修改权限。
            UIHelper.Label("labResult").Text($"已保存：{id}；{Data.Name}；{Data.Quantity}");
            return UIHelper.Result();
        }
    }

    public class OrderInput
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Range(1, 100)]
        [Display(Name = "数量")]
        public int Quantity { get; set; }
    }
}
