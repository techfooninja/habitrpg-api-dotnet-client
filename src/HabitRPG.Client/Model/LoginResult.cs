using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class LoginResult
    {
        [JsonProperty("_id")]
        public string UserId { get; protected set; }

        [JsonProperty("apiToken")]
        public string ApiToken { get; protected set; }

        [JsonProperty("newUser")]
        public bool IsNewUser { get; protected set; }
    }
}
