using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitRPG.Client.Model
{
    public class QuestProgress
    {
        [JsonProperty("progressDelta")]
        public double Progress { get; set; }

        [JsonProperty("collection")]
        public int Collection{ get; set; }
    }
}
