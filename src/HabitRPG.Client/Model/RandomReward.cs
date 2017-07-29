using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class RandomReward
    {
        [JsonProperty("crit")]
        public string CriticalHit { get; set; }

        [JsonProperty("streakBonus")]
        public string StreakBonus { get; set; }

        [JsonProperty("drop")]
        public Drop Drop { get; set; }
    }
}