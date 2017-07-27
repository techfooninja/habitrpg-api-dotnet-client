namespace HabitRPG.Client.Model
{
    using System.Runtime.Serialization;

    public enum TaskQuery
    {
        [EnumMember(Value = "")]
        All,

        [EnumMember(Value = "habits")]
        Habits,

        [EnumMember(Value = "dailys")]
        Dailies,

        [EnumMember(Value = "todos")]
        Todos,

        [EnumMember(Value = "rewards")]
        Rewards,

        [EnumMember(Value = "completedTodos")]
        CompletedTodos
    }
}
