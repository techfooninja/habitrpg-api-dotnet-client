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
        protected TaskItem() : this(TaskType.Daily)
        {
            // This method is used for serialization
        }

        public TaskItem(TaskType type)
        {
            TagGuids = new List<Guid>();
            Priority = Difficulty.Easy;
            Type = type;
        }

        #region Properties

        [JsonProperty("id")]
        public Guid Id { get; protected set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("createdAt")]
        public DateTime? DateCreated { get; protected set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("tags")]
        protected List<Guid> TagGuids { get; set; }

        public List<Tag> Tags
        {
            get
            {
                return Tag.AllTags.Where(t => TagGuids.SingleOrDefault(g => t.Id == g) != null).ToList();
            }
        }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("priority")]
        public Difficulty Priority { get; set; }

        [JsonProperty("attribute")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Attribute Attribute { get; set; }

        [JsonProperty("challenge")]
        public Challenge Challenge { get; set; }

        [JsonProperty("type")]
        public TaskType Type { get; protected set; }

        #endregion Properties

        private void CopyFrom(TaskItem item)
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

        public async Task<ScoreResult> ScoreAsync(Direction direction)
        {
            var response = await HttpClient.PostAsync(string.Format("tasks/{0}/score/{1}", Id, direction.ToString().ToLower()), null);
            response.EnsureSuccessStatusCode();
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

            response.EnsureSuccessStatusCode();

            // Update this task
            CopyFrom(GetResult<TaskItem>(response));
        }

        public async Task DeleteAsync()
        {
            var response = await HttpClient.DeleteAsync(string.Format("tasks/{0}", Id));
            response.EnsureSuccessStatusCode();
            // TODO: Dispose?
        }

        public async Task AddTag(Tag tag)
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
            response.EnsureSuccessStatusCode();
            CopyFrom(GetResult<TaskItem>(response));
        }

        public async Task DeleteTag(Tag tag)
        {
            var response = await HttpClient.DeleteAsync(String.Format("tasks/{0}/tags/{1}", Id, tag.Id));
            response.EnsureSuccessStatusCode();
            CopyFrom(GetResult<TaskItem>(response));
        }

        public static async Task<List<TaskItem>> GetTasksAsync(TaskQuery query = TaskQuery.All)
        {
            var response = await HttpClient.GetAsync(String.Format("tasks/user?type=", query));
            response.EnsureSuccessStatusCode();
            return GetResult<List<TaskItem>>(response);
        }

        public static async Task<TaskItem> GetTaskAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("id");
            }

            var response = await HttpClient.GetAsync(string.Format("tasks/{0}", id));
            response.EnsureSuccessStatusCode();
            return GetResult<TaskItem>(response);
        }
    }
}