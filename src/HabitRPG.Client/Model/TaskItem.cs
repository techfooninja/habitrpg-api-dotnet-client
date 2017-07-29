namespace HabitRPG.Client.Model
{
    using HabitRPG.Client.Extensions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    
    public class TaskItem : HabiticaObject
    {
        protected TaskItem()
        {
            TagGuids = new List<Guid>();
            Priority = Difficulty.Easy;
        }

        #region Properties

        [JsonProperty("id")]
        public Guid Id { get; protected set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("createdAt")]
        public DateTime? DateCreated { get; protected set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("tags")]
        protected List<Guid> TagGuids { get; set; }

        [JsonIgnore]
        public IReadOnlyList<Tag> Tags
        {
            get
            {
                return TagGuids
                        .Where(k => InternalAllTags.ContainsKey(k))
                        .Select(k => InternalAllTags[k]).ToList();
            }
        }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("priority")]
        protected float InternalPriority { get; set; }

        [JsonIgnore()]
        public Difficulty Priority
        {
            get
            {
                return InternalPriority.GetDifficulty();
            }
            set
            {
                InternalPriority = value.GetValue();
            }
        }

        [JsonProperty("attribute")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Attribute Attribute { get; set; }

        [JsonProperty("challenge", NullValueHandling = NullValueHandling.Ignore)]
        public Challenge Challenge { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        protected TaskType Type { get; set; }

        [JsonIgnore]
        protected static Dictionary<Guid, Tag> InternalAllTags { get; private set; }

        [JsonIgnore]
        public static IReadOnlyList<Tag> AllTags
        {
            get
            {
                return InternalAllTags.Values.ToList();
            }
        }

        /*
         * TODO
         * 
         * Missing Properties:
         *   Reminders
         * 
         */ 

        #endregion Properties

        protected virtual void CopyFrom(TaskItem item)
        {
            Id = item.Id;
            DateCreated = item.DateCreated;
            Text = item.Text;
            Notes = item.Notes;
            TagGuids = item.TagGuids;
            Value = item.Value;
            Priority = item.Priority;
            Attribute = item.Attribute;
            Challenge = item.Challenge;
            Type = item.Type;
        }

        public async Task<ScoreResult> ScoreAsync(Direction direction = Direction.Up)
        {
            var response = await HttpClient.PostAsync(string.Format("tasks/{0}/score/{1}", Id, direction.ToString().ToLower()), null);
            var updatedTask = await GetAsync(Id);
            CopyFrom(updatedTask);

            return GetResult<ScoreResult>(response);
        }

        public async Task SaveAsync()
        {
            HttpResponseMessage response = null;

            if (Id == Guid.Empty)
            {
                // If the Id is empty, then this is a new task - create it
                response = await HttpClient.PostAsJsonAsync("tasks/user", this);
            }
            else
            {
                // Otherwise, just update the task
                response = await HttpClient.PutAsJsonAsync(string.Format("tasks/{0}", Id), this);
            }

            // Update this task
            CopyFrom(GetResult<TaskItem>(response));
        }

        public async Task DeleteAsync()
        {
            var response = await HttpClient.DeleteAsync(string.Format("tasks/{0}", Id));
            response.EnsureSuccessStatusCode();
            // TODO: Dispose?
        }

        public async Task AddTagAsync(Tag tag)
        {
            if (tag.Id == Guid.Empty)
            {
                await tag.SaveAsync();
            }

            if (Id == Guid.Empty)
            {
                await SaveAsync();
            }

            var response = await HttpClient.PostAsync(String.Format("tasks/{0}/tags/{1}", Id, tag.Id), null);
            CopyFrom(GetResult<TaskItem>(response));

            var result = InternalAllTags[tag.Id];

            if (result == null)
            {
                InternalAllTags.Add(tag.Id, tag);
            }
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            var response = await HttpClient.DeleteAsync(String.Format("tasks/{0}/tags/{1}", Id, tag.Id));
            CopyFrom(GetResult<TaskItem>(response));
        }

        public static async Task<List<TaskItem>> GetAllAsync(TaskQuery query = TaskQuery.All)
        {
            await GetTagsAsync();
            HttpResponseMessage response = null;
            if (query == TaskQuery.All)
            {
                response = await HttpClient.GetAsync("tasks/user");
            }
            else
            {
                response = await HttpClient.GetAsync(String.Format("tasks/user?type={0}", query.GetDisplayString()));
            }
            
            return GetResult<List<TaskItem>>(response);
        }

        public static async Task<TaskItem> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("id");
            }

            await GetTagsAsync();

            var response = await HttpClient.GetAsync(string.Format("tasks/{0}", id));
            return GetResult<TaskItem>(response);
        }

        public static async Task<List<Tag>> GetTagsAsync()
        {
            var response = await HttpClient.GetAsync("tags");
            var result = GetResult<List<Tag>>(response);

            if (InternalAllTags == null)
            {
                InternalAllTags = result.ToDictionary(t => t.Id);
            }
            else
            {
                InternalAllTags.Clear();
                foreach (var item in result)
                {
                    InternalAllTags.Add(item.Id, item);
                }
            }

            return result;
        }

        public static async Task ClearCompletedTodos()
        {
            var response = await HttpClient.PostAsync("tasks/clearCompletedTodos", null);
            response.EnsureSuccessStatusCode();
        }
    }
}