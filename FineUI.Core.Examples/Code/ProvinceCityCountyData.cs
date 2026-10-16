using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples
{
    /// <summary>省市县示例的固定编码和显示名称。</summary>
    public static class ProvinceCityCountyData
    {
        private static readonly JObject Data = JObject.Parse(File.ReadAllText(
            FineUI.Core.PageContext.MapPath("~/App_Data/province-city-county.json")));

        private static readonly JsonSerializerSettings ScriptDataSettings = new JsonSerializerSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeHtml
        };

        /// <summary>全部省份，按示例原有的展示顺序排列；Code 是表单保存的值，Name 是显示文本。</summary>
        public static IReadOnlyList<ProvinceCityCountyItem> Provinces { get; } = ReadOptions((JArray)Data["provinces"]);

        /// <summary>省份编码到城市编码及名称的列表，供浏览器端的省市联动读取。</summary>
        public static JObject CitiesByProvince => (JObject)Data["cities"];

        /// <summary>城市编码到县区编码及名称的列表，供树形网格读取。</summary>
        public static JObject CountiesByCity => (JObject)Data["counties"];

        /// <summary>读取指定省份的城市；找不到省份时返回空列表。</summary>
        public static IReadOnlyList<ProvinceCityCountyItem> Cities(string provinceCode)
        {
            return String.IsNullOrEmpty(provinceCode)
                ? Array.Empty<ProvinceCityCountyItem>()
                : ReadOptions(Data["cities"]?[provinceCode] as JArray);
        }

        /// <summary>读取指定城市的县区；找不到城市时返回空列表。</summary>
        public static IReadOnlyList<ProvinceCityCountyItem> Counties(string cityCode)
        {
            return String.IsNullOrEmpty(cityCode)
                ? Array.Empty<ProvinceCityCountyItem>()
                : ReadOptions(Data["counties"]?[cityCode] as JArray);
        }

        /// <summary>按保存的省份编码查显示名称；空值和无效编码显示为空。</summary>
        public static string ProvinceName(string code)
        {
            return FindName(Provinces, code);
        }

        /// <summary>仅供显示名称的文本框再次打开选省窗口时查回编码；找不到则不预选。</summary>
        public static string ProvinceCode(string name)
        {
            foreach (ProvinceCityCountyItem province in Provinces)
            {
                if (province.Name == name)
                {
                    return province.Code;
                }
            }
            return String.Empty;
        }

        /// <summary>按保存的城市编码查显示名称；城市编码的前三位是所属省份。</summary>
        public static string CityName(string code)
        {
            return String.IsNullOrEmpty(code) || code.Length < 3
                ? String.Empty
                : FindName(Cities(code.Substring(0, 3)), code);
        }

        /// <summary>按保存的县区编码查显示名称；县区编码的前六位是所属城市。</summary>
        public static string CountyName(string code)
        {
            return String.IsNullOrEmpty(code) || code.Length < 6
                ? String.Empty
                : FindName(Counties(code.Substring(0, 6)), code);
        }

        /// <summary>把省市数据写入非执行 JSON 数据块；HTML 特殊字符会转义，避免意外结束 script 标签。</summary>
        public static string ProvinceCityJson => JsonConvert.SerializeObject(new JObject
        {
            ["provinces"] = Data["provinces"],
            ["cities"] = Data["cities"]
        }, ScriptDataSettings);

        private static IReadOnlyList<ProvinceCityCountyItem> ReadOptions(JArray items)
        {
            var result = new List<ProvinceCityCountyItem>();
            if (items == null)
            {
                return result;
            }

            foreach (JArray item in items)
            {
                result.Add(new ProvinceCityCountyItem(item[0].Value<string>(), item[1].Value<string>()));
            }
            return result;
        }

        private static string FindName(IReadOnlyList<ProvinceCityCountyItem> options, string code)
        {
            foreach (ProvinceCityCountyItem option in options)
            {
                if (option.Code == code)
                {
                    return option.Name;
                }
            }
            return String.Empty;
        }
    }

    /// <summary>下拉列表保存 Code，向用户显示 Name。</summary>
    public sealed class ProvinceCityCountyItem
    {
        public ProvinceCityCountyItem(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }

        public string Name { get; }
    }
}
