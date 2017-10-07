using System.Runtime.Serialization;

namespace HabitRPG.Client.Model
{
    public enum DropType
    {
        [EnumMember(Value = "Food")]
        Food,

        [EnumMember(Value = "HatchingPotion")]
        HatchingPotion,

        [EnumMember(Value = "Egg")]
        Egg
    }
}
