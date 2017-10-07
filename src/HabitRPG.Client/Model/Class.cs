using System.Runtime.Serialization;

namespace HabitRPG.Client.Model
{
    public enum Class
    {
        [EnumMember(Value = "warrior")]
        Warrior,

        [EnumMember(Value = "rogue")]
        Rogue,

        [EnumMember(Value = "wizard")]
        Mage,

        [EnumMember(Value = "healer")]
        Healer
    }
}
