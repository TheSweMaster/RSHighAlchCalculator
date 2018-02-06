using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RSHighAlchCalculator.Models
{
    public partial class RsExchangeData
    {
        [JsonProperty("members")]
        public bool Members { get; set; }

        [JsonProperty("buy_average")]
        public long BuyAverage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overall_average")]
        public long OverallAverage { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sell_average")]
        public long SellAverage { get; set; }

        [JsonProperty("sp")]
        public long Sp { get; set; }
    }

    public partial class RsExchangeData
    {
        public static Dictionary<string, RsExchangeData> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, RsExchangeData>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, RsExchangeData> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
