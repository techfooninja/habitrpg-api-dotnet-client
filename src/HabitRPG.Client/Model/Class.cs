namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum Class
    {
        [EnumMember(Value = "warrior")]
        Warrior,

        [EnumMember(Value = "rogue")]
        Rogue,

        [EnumMember(Value = "wizard")]
        Wizard,

        [EnumMember(Value = "healer")]
        Healer
    }
}
