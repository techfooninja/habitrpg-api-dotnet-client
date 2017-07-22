using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class LoginCredential
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
