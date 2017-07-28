namespace HabitRPG.Client.Model
{
    using System;
    using Newtonsoft.Json;
    using HabitRPG.Client.Converters;

    public class History : HabiticaObject
    {
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
