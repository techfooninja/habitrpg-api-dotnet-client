namespace HabitRPG.Client.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class TaskOrder
    {
        #region Properties

        [JsonProperty("dailys")]
        public List<Guid> DailiesOrder { get; set; }

        [JsonProperty("habits")]
        public List<Guid> HabitsOrder { get; set; }

        [JsonProperty("todos")]
        public List<Guid> TodosOrder { get; set; }

        [JsonProperty("rewards")]
        public List<Guid> RewardsOrder { get; set; }

        #endregion Properties
    }
}