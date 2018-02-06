using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSHighAlchCalculator.Names
{ 
    public partial class NameListModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("store")]
        public long Store { get; set; }
    }

    public partial class NameListModel
    {
        public static Dictionary<string, NameListModel> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, NameListModel>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, NameListModel> self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
