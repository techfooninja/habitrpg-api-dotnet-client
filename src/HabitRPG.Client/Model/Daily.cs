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
            ConfirmBeforeCron = true;
        }

        #region Properties

        [JsonProperty("history", NullValueHandling = NullValueHandling.Ignore)]
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
        [JsonConverter(typeof(StringEnumConverter))]
        public Frequency Frequency { get; set; }

        [JsonProperty("repeat", NullValueHandling = NullValueHandling.Ignore)]
        public Repeat Repeat { get; set; }

        [JsonProperty("streak")]
        public int Streak { get; set; }
        
        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? StartDate { get; set; }

        [JsonProperty("yesterDaily")]
        public bool ConfirmBeforeCron { get; set; }

        /*
         * TODO
         * 
         * Missing Properties:
         *   daysOfMonth
         *   weeksOfMonth
         *   nextDue
         *   isDue
         * 
         */

        #endregion Properties

        protected override void CopyFrom(TaskItem item)
        {
            Daily daily = (Daily)item;
            base.CopyFrom(item);
            UpdateCollection<History>(InternalHistory, daily.InternalHistory);
            Completed = daily.Completed;
            Repeat = daily.Repeat;
            Streak = daily.Streak;
            StartDate = daily.StartDate;
            ConfirmBeforeCron = daily.ConfirmBeforeCron;
        }
    }
}