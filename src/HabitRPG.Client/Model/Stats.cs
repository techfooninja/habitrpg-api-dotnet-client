using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class Stats : StatsBase
    {
        [JsonProperty("buffs")]
        public Buffs Buffs { get; set; }

        [JsonProperty("class")]
        public Class Class { get; set; }

        [JsonProperty("exp")]
        public double Experience { get; set; }

        [JsonProperty("gp")]
        public double Gold { get; set; }

        [JsonProperty("hp")]
        public double HitPoints { get; set; }

        [JsonProperty("lvl")]
        public int Level { get; set; }

        [JsonProperty("mp")]
        public double ManaPoints { get; set; }

        [JsonProperty("training")]
        public StatsBase Training { get; set; }
    }
}
