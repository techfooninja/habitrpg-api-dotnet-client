namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum Difficulty
    {
        [EnumMember(Value = "0.1")]
        Trivial,

        [EnumMember(Value = "1")]
        Easy,

        [EnumMember(Value = "1.5")]
        Medium,

        [EnumMember(Value = "2")]
        Hard
    }
}
