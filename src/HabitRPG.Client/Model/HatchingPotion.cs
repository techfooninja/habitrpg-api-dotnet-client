using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitRPG.Client.Model
{
    public class HatchingPotion : Drop
    {
        [JsonProperty("premium")]
        public bool IsPremium { get; set; }

        [JsonProperty("limited")]
        public bool IsLimited { get; set; }
    }
}
