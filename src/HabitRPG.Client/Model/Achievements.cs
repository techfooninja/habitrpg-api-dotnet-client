using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using HabitRPG.Client.Converters;

namespace HabitRPG.Client.Model
{
    public class Achievements
    {
        public Achievements()
        {
            UltimateGearSets = new Dictionary<Class, bool>();
            Challenges = new List<string>();
            Quests = new Dictionary<string, int>();
        }

        [JsonProperty("challenges")]
        public List<string> Challenges { get; set; }

        [JsonProperty("perfect")]
        public int PerfectDays { get; set; }

        [JsonProperty("streak")]
        public int Streaks { get; set; }

        [JsonProperty("quests")]
        public Dictionary<string, int> Quests { get; set; }

        [JsonProperty("habitBirthdays")]
        public int HabitBirthdays { get; set; }

        [JsonProperty("habiticaDays")]
        public bool HabiticaDays { get; set; }

        [JsonProperty("beastMaster")]
        public bool BeastMaster { get; set; }

        [JsonProperty("mountMaster")]
        public bool MountMaster { get; set; }

        [JsonProperty("triadBingo")]
        public bool TriadBingo { get; set; }

        [JsonProperty("partyUp")]
        public bool PartyUp { get; set; }

        [JsonProperty("partyOn")]
        public bool PartyOn { get; set; }

        [JsonProperty("joinedGuild")]
        public bool JoinedGuild { get; set; }

        [JsonProperty("joinedChallenge")]
        public bool JoinedChallenge { get; set; }

        [JsonProperty("royallyLoyal")]
        public bool RoyallyLoyal { get; set; }

        [JsonProperty("invitedFriend")]
        public bool InvitedFriend { get; set; }

        [JsonProperty("seafoam")]
        public int Seafoams { get; set; }

        [JsonProperty("ultimateGearSets")]
        public Dictionary<Class, bool> UltimateGearSets { get; set; }

        /*
         * 
         * Missing:
         *   Card achievements
         *   Rebirths
         * 
         */ 
    }
}