using Newtonsoft.Json;

namespace HabitRPG.Client.Model
{
    public class StatsBase
    {
        [JsonProperty("con")]
        public double Constitution { get; set; }

        [JsonProperty("int")]
        public double Intelligence { get; set; }

        [JsonProperty("per")]
        public double Perception { get; set; }

        [JsonProperty("str")]
        public double Strength { get; set; }

        public static StatsBase operator +(StatsBase one, StatsBase two)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution + two.Constitution;
            result.Intelligence = one.Intelligence + two.Intelligence;
            result.Perception = one.Perception + two.Perception;
            result.Strength = one.Strength + two.Strength;
            return result;
        }

        public static StatsBase operator -(StatsBase one, StatsBase two)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution - two.Constitution;
            result.Intelligence = one.Intelligence - two.Intelligence;
            result.Perception = one.Perception - two.Perception;
            result.Strength = one.Strength - two.Strength;
            return result;
        }

        public static StatsBase operator +(StatsBase one, int val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution + val;
            result.Intelligence = one.Intelligence + val;
            result.Perception = one.Perception + val;
            result.Strength = one.Strength + val;
            return result;
        }

        public static StatsBase operator +(StatsBase one, double val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution + val;
            result.Intelligence = one.Intelligence + val;
            result.Perception = one.Perception + val;
            result.Strength = one.Strength + val;
            return result;
        }

        public static StatsBase operator -(StatsBase one, int val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution - val;
            result.Intelligence = one.Intelligence - val;
            result.Perception = one.Perception - val;
            result.Strength = one.Strength - val;
            return result;
        }

        public static StatsBase operator *(StatsBase one, int val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution * val;
            result.Intelligence = one.Intelligence * val;
            result.Perception = one.Perception * val;
            result.Strength = one.Strength * val;
            return result;
        }

        public static StatsBase operator *(StatsBase one, double val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution * val;
            result.Intelligence = one.Intelligence * val;
            result.Perception = one.Perception * val;
            result.Strength = one.Strength * val;
            return result;
        }

        public static StatsBase operator /(StatsBase one, int val)
        {
            var result = new StatsBase();
            result.Constitution = one.Constitution / val;
            result.Intelligence = one.Intelligence / val;
            result.Perception = one.Perception / val;
            result.Strength = one.Strength / val;
            return result;
        }
    }
}
