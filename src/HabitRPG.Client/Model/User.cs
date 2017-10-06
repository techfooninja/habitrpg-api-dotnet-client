using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace HabitRPG.Client.Model
{
    public class User : Member
    {
        [JsonProperty("balance")]
        public double Gems { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("challenges")]
        public List<Guid> Challenges { get; set; }

        [JsonProperty("tasksOrder")]
        public TaskOrder TasksOrder { get; set; }
    }
}