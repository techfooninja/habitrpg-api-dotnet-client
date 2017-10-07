using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HabitRPG.Client.Model
{
    public class Drop
    {
        [JsonProperty("dialog")]
        public string Dialog { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DropType Type { get; set; }
    }
}