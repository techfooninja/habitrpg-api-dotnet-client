using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class Food : Drop
    {
        [JsonProperty("canBuy")]
        public bool CanBuy { get; set; }
    }
}