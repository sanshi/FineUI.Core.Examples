using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class TreeSelectionModel : PageBase
    {
        public IActionResult OnPostReadNode(JObject selectedNode)
        {
            UIHelper.Label("Result").Text($"节点：{selectedNode?["id"]}；{selectedNode?["text"]}");
            return UIHelper.Result();
        }

        public IActionResult OnPostSelectHefei()
        {
            UIHelper.Tree("Tree1").SelectedNodeID("hefei");
            return UIHelper.Result();
        }
    }
}
