namespace HabitRPG.Client.Model
{
    using Newtonsoft.Json;

    public class ScoreResult : Stats
    {
        /// <summary>
        /// Delta of the added Task.Value
        /// </summary>RandomReward
        [JsonProperty("delta")]
        public double Delta { get; set; }

        [JsonProperty("_tmp")]
        public RandomReward RandomReward { get; set; }

        [JsonProperty("points")]
        public double Points { get; set; }
    }
}
