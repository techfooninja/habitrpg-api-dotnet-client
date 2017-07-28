namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum Frequency
    {
        [EnumMember(Value = "weekly")]
        Weekly,

        [EnumMember(Value = "daily")]
        Daily
    }
}
