using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class RandomReward
    {
        [JsonProperty("crit")]
        public string Crit { get; set; }

        [JsonProperty("streakBonus")]
        public double StreakBonus { get; set; }

        [JsonProperty("drop")]
        public Drop Drop { get; set; }

        [JsonProperty("quest")]
        public QuestProgress QuestProgress { get; set; }
    }
}