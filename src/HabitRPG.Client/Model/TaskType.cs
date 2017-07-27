namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum TaskType
    {
        All,

        [EnumMember(Value = "habit")]
        Habit,

        [EnumMember(Value = "daily")]
        Daily,

        [EnumMember(Value = "todo")]
        Todo,

        [EnumMember(Value = "reward")]
        Reward
    }
}
