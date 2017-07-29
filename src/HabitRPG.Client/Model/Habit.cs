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

        [JsonProperty("up")]
        public bool Up { get; set; }

        [JsonProperty("down")]
        public bool Down { get; set; }

        [JsonProperty("counterUp")]
        public int CounterUp { get; protected set; }

        [JsonProperty("counterDown")]
        public int CounterDown { get; protected set; }

        #endregion Properties

        protected override void CopyFrom(TaskItem item)
        {
            Habit habit = (Habit)item;
            base.CopyFrom(item);
            UpdateCollection<History>(InternalHistory, habit.InternalHistory);
            Up = habit.Up;
            Down = habit.Down;
            CounterUp = habit.CounterUp;
            CounterDown = habit.CounterDown;
        }
    }
}