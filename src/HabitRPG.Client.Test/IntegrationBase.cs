using System;
using NUnit.Framework;
using HabitRPG.Client.Model;

namespace HabitRPG.Client.Test
{
    [TestFixture]
    public class IntegrationBase
    {
        protected readonly HabiticaClient _client;

        public IntegrationBase()
        {
            HabitRpgConfiguration = new HabitRpgConfiguration
            {
                UserId = Guid.Parse("cfd9941b-037c-45bc-9d86-320067a4de3e"),
                ApiToken = Guid.Parse("255aa387-ff1c-4804-ad3e-a6382d60c809"),
                ServiceUri = new Uri(@"https://habitica.com/")
            };

            _client = new HabiticaClient(HabitRpgConfiguration);
        }

        public HabitRpgConfiguration HabitRpgConfiguration { get; set; }

        [TearDown]
        public void TearDown()
        {
            var response = TaskItem.GetAllAsync();
            response.Wait();

            foreach (TaskItem item in response.Result)
            {
                var itemResponse = item.DeleteAsync();
                itemResponse.Wait();
            }
        }

        #region Helper functions

        protected static Daily CreateDaily()
        {
            var daily = new Daily
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Model.Attribute.Strength,
                Completed = false,
                Repeat = new Repeat
                {
                    Friday = false,
                    Wednesday = false
                },
                CollapseChecklist = false,
                Streak = 32
            };

            return daily;
        }

        protected static Habit CreateHabit()
        {
            var habitTask = new Habit
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Model.Attribute.Strength,
                Up = true,
                Down = true
            };

            return habitTask;
        }

        protected static Todo CreateTodo()
        {
            var habitTask = new Todo
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Model.Attribute.Strength,
                Completed = true,
                Archived = true,
                DateCompleted = DateTime.Now,
                DueDate = DateTime.Now,
                CollapseChecklist = true
            };

            return habitTask;
        }

        protected static Reward CreateReward()
        {
            var reward = new Reward
            {
                Text = "Main Task: " + DateTime.Now,
                Notes = "Notes",
                Value = 1,
                Priority = Difficulty.Hard,
                Attribute = Model.Attribute.Strength
            };

            return reward;
        }

        #endregion Helper functions
    }
}
