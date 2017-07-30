namespace HabitRPG.Client.Model
{
    using System;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public class Member : HabiticaObject
    {
        #region Properties

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("items")]
        public Items Items { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }

        [JsonProperty("contributor")]
        public Contributor Contributor { get; set; }

        [JsonProperty("auth")]
        public Authentication Authentication { get; set; }

        [JsonProperty("achievements")]
        public Achievements Achievements { get; set; }

        [JsonIgnore]
        public Uri AvatarUri
        {
            // TODO: Is there a better way to do this where an image object is returned?
            get
            {
                return new Uri(String.Format("{0}/export/avatar-{1}.png", Configuration.ServiceUri, Id));
            }
        }

        /*
         * 
         * Missing:
         * 
         *   Party
         */

        #endregion Properties

        public static async Task<Member> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("id");
            }

            var response = await HttpClient.GetAsync(string.Format("members/{0}", id));
            return GetResult<Member>(response);
        }
    }
}