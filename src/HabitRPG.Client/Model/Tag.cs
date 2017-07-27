namespace HabitRPG.Client.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HabitRPG.Client.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class Tag : HabiticaObject
    {
        public static List<Tag> AllTags = new List<Tag>();

        #region Properties

        [JsonProperty("id")]
        public Guid Id { get; protected set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("challenge")]
        public string Challenge { get; set; }

        #endregion Properties

        private void CopyFrom(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
            Challenge = tag.Challenge;
        }

        public async Task SaveAsync()
        {
            HttpResponseMessage response = null;

            if (Id == Guid.Empty)
            {
                // If the Id is empty, then this is a new tag - create it
                response = await HttpClient.PostAsJsonAsync("tags", this);
            }
            else
            {
                // Otherwise, just update the task
                response = await HttpClient.PutAsJsonAsync(string.Format("tags/{0}", Id), this);
            }

            response.EnsureSuccessStatusCode();

            // Update this task
            CopyFrom(GetResult<Tag>(response));
        }

        public async Task DeleteAsync()
        {
            var response = await HttpClient.DeleteAsync(string.Format("tags/{0}", Id));
            response.EnsureSuccessStatusCode();
            // TODO: Dispose?
        }

        public static async Task UpdateTagsAsync()
        {
            var response = await HttpClient.GetAsync("tags");
            response.EnsureSuccessStatusCode();
            var result = GetResult<List<Tag>>(response);

            if (AllTags == null)
            {
                AllTags = result;
            }
            else
            {
                foreach (var oldTag in AllTags)
                {
                    if (result.SingleOrDefault(t => t.Id == oldTag.Id) == null)
                    {
                        AllTags.Remove(oldTag);
                    }
                }

                foreach (var newTag in result)
                {
                    if (AllTags.SingleOrDefault(t => t.Id == newTag.Id) == null)
                    {
                        AllTags.Add(newTag);
                    }
                }
            }
        }
    }
}
