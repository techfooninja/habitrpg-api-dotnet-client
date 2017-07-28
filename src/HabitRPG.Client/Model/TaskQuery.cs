namespace HabitRPG.Client.Model
{
    public enum TaskQuery
    {
        All,
        Habits,
        Dailies,
        Todos,
        Rewards,
        CompletedTodos
    }

    internal static class TaskQueryExtensions
    {
        internal static string GetDisplayString(this TaskQuery query)
        {
            switch (query)
            {
                case TaskQuery.All: return string.Empty;
                case TaskQuery.Habits: return "habits";
                case TaskQuery.Dailies: return "dailys";
                case TaskQuery.Todos: return "todos";
                case TaskQuery.Rewards: return "rewards";
                case TaskQuery.CompletedTodos: return "completedTodos";
                default: return string.Empty;
            }
        }
    }
}
