using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class EChartsDynamicBarIFrameModel : BaseModel
    {
        //public void OnGet()
        //{
        //    var barOptions = String.Empty;
        //    var paramType = Request.Query["type"];
        //    if (String.IsNullOrEmpty(paramType) || paramType == "sales")
        //    {
        //        //{
        //        //    "title": {
        //        //        "text": "销量图表"
        //        //    },
        //        //    "xAxis": {
        //        //        "type": "category",
        //        //        "data": ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
        //        //    },
        //        //    "yAxis": {
        //        //        "type": "value"
        //        //    },
        //        //    "series": [{
        //        //        "name": "销量",
        //        //        "type": "bar",
        //        //        "data": [5.0, 20.0, 36.0, 10.0, 10.0, 20.0]
        //        //    }]
        //        //}
        //        barOptions = "{\"color\":[\"#5793f3\"],\"title\":{\"text\":\"销量图表\"},\"xAxis\":{\"type\":\"category\",\"data\":[\"衬衫\",\"羊毛衫\",\"雪纺衫\",\"裤子\",\"高跟鞋\",\"袜子\"]},\"yAxis\":{\"type\":\"value\"},\"series\":[{\"name\":\"销量\",\"type\":\"bar\",\"data\":[5.0,20.0,36.0,10.0,10.0,20.0]}]}";
        //    }
        //    else
        //    {
        //        //{
        //        //    "title": {
        //        //        "text": "产量图表"
        //        //    },
        //        //    "xAxis": {
        //        //        "type": "category",
        //        //        "data": ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
        //        //    },
        //        //    "yAxis": {
        //        //        "type": "value"
        //        //    },
        //        //    "series": [{
        //        //        "name": "销量",
        //        //        "type": "bar",
        //        //        "data": [6.0, 18.0, 17.0, 30.0, 20.0, 5.0]
        //        //    }]
        //        //}
        //        barOptions = "{\"color\":[\"#d14a61\"],\"title\":{\"text\":\"产量图表\"},\"xAxis\":{\"type\":\"category\",\"data\":[\"衬衫\",\"羊毛衫\",\"雪纺衫\",\"裤子\",\"高跟鞋\",\"袜子\"]},\"yAxis\":{\"type\":\"value\"},\"series\":[{\"name\":\"销量\",\"type\":\"bar\",\"data\":[6.0,18.0,17.0,35.0,20.0,5.0]}]}";
        //    }

        //    ViewBag.BarOptions = barOptions;

        //}

    }
}