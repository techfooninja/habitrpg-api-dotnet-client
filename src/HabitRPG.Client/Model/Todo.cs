namespace HabitRPG.Client.Model
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Todo : ChecklistTaskItem
    {
        public Todo() : base()
        {
            Type = TaskType.Todo;
        }

        #region Properties

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("dateCompleted")]
        public DateTime? DateCompleted { get; set; }

        [JsonProperty("date")]
        public DateTime? DueDate { get; set; }

        #endregion Properties

        protected override void CopyFrom(TaskItem item)
        {
            Todo todo = (Todo)item;
            base.CopyFrom(item);
            Completed = todo.Completed;
            Archived = todo.Archived;
            DateCompleted = todo.DateCompleted;
            DueDate = todo.DueDate;
        }
    }
}