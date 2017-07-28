namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum Attribute
    {
        [EnumMember(Value = "str")]
        Strength,

        [EnumMember(Value = "per")]
        Perception,

        [EnumMember(Value = "con")]
        Constitution,

        [EnumMember(Value = "int")]
        Intelligence,
    }
}
