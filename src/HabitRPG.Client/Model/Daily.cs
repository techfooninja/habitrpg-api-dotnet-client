namespace HabitRPG.Client.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System;
    using Newtonsoft.Json.Converters;

    public class Daily : ChecklistTaskItem
    {
        public Daily() : base()
        {
            Type = TaskType.Daily;
            InternalHistory = new List<History>();
        }

        #region Properties

        [JsonProperty("history")]
        protected List<History> InternalHistory { get; set; }

        [JsonIgnore]
        public IReadOnlyList<History> History
        {
            get
            {
                return InternalHistory;
            }
        }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("frequency")]
        public Frequency Frequency { get; set; }

        [JsonProperty("repeat")]
        public Repeat Repeat { get; set; }

        [JsonProperty("streak")]
        public int Streak { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        #endregion Properties

        protected override void CopyFrom(TaskItem item)
        {
            Daily daily = (Daily)item;
            base.CopyFrom(item);
            UpdateCollection<History>(InternalHistory, daily.InternalHistory);
            Completed = daily.Completed;
            Repeat = daily.Repeat;
            Streak = daily.Streak;
        }
    }
}