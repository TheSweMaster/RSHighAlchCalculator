using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RSHighAlchCalculator.Price
{
    public partial class RsPriceData
    {
        [JsonProperty("overall")]
        [DisplayName("Overall Price")]
        public long Overall { get; set; }

        [JsonProperty("buying")]
        [DisplayName("Buying Price")]
        public long Buying { get; set; }

        [JsonProperty("selling")]
        [DisplayName("Selling Price")]
        public long Selling { get; set; }

        [JsonProperty("buyingQuantity")]
        [DisplayName("Buying Quantity")]
        public long BuyingQuantity { get; set; }

        [JsonProperty("sellingQuantity")]
        [DisplayName("Selling Quantity")]
        public long SellingQuantity { get; set; }
    }

    public partial class RsPriceData
    {
        public static RsPriceData FromJson(string json) => JsonConvert.DeserializeObject<RsPriceData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RsPriceData self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
