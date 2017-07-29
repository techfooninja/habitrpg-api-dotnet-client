namespace HabitRPG.Client.Model
{
    public enum Difficulty
    {
        Trivial,
        Easy,
        Medium,
        Hard
    }

    internal static class DifficultyExtensions
    {
        internal static float GetValue(this Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Trivial: return 0.1f;
                case Difficulty.Easy: return 1f;
                case Difficulty.Medium: return 1.5f;
                case Difficulty.Hard: return 2f;
                default: return 0f;
            }
        }

        internal static Difficulty GetDifficulty(this float value)
        {
            switch (value)
            {
                case 0.1f: return Difficulty.Trivial;
                case 1f: return Difficulty.Easy;
                case 1.5f: return Difficulty.Medium;
                case 2f: return Difficulty.Hard;
                default: return Difficulty.Medium;
            }
        }
    }
}
