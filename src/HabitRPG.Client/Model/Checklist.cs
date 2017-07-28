namespace HabitRPG.Client.Model
{
    using System;
    using Newtonsoft.Json;

    public class Checklist : HabiticaObject
    {
        #region Properties

        [JsonProperty("completed")]
        public bool Completed { get; protected set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; protected set; }

        #endregion Properties

        internal void CopyFrom(Checklist checklist)
        {
            Completed = checklist.Completed;
            Text = checklist.Text;
            Id = checklist.Id;
        }
    }
}
