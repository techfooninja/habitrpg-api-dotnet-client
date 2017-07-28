using System.Collections.Generic;
using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class Habit : TaskItem
    {
        public Habit() : base()
        {
            Type = TaskType.Habit;
            InternalHistory = new List<History>();
            Up = true;
            Down = true;
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

        [JsonProperty("up")]
        public bool Up { get; set; }

        [JsonProperty("down")]
        public bool Down { get; set; }

        #endregion Properties

        protected override void CopyFrom(TaskItem item)
        {
            Habit habit = (Habit)item;
            base.CopyFrom(item);
            UpdateCollection<History>(InternalHistory, habit.InternalHistory);
            Up = habit.Up;
            Down = habit.Down;
        }
    }
}