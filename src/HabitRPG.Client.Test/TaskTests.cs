using HabitRPG.Client.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Attribute = HabitRPG.Client.Model.Attribute;
using TaskItem = HabitRPG.Client.Model.TaskItem;

namespace HabitRPG.Client.Test
{
    [TestFixture]
    public class TaskTests : IntegrationBase
    {
        private readonly HabiticaClient _client;

        public TaskTests()
        {
            _client = new HabiticaClient(HabitRpgConfiguration);
        }

        [Test]
        public void CreateNewTodo()
        {
            var todo = CreateTodo();
            var response = todo.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, todo.Id);
        }

        [Test]
        public void CreateNewHabit()
        {
            var habit = CreateHabit();
            var response = habit.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, habit.Id);
        }

        [Test]
        public void CreateNewDaily()
        {
            var daily = CreateDaily();
            var response = daily.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, daily.Id);
        }

        /*[Test]
        public void Should_create_new_habit_task()
        {
            // Setup
            var habit = CreateHabit();

            // Action
            var response = _client.CreateTaskAsync(habit);
            response.Wait();

            // Verify the result
            AssertTask(habit, response.Result);

            Assert.AreEqual(habit.Down, response.Result.Down);
            Assert.AreEqual(habit.Up, response.Result.Up);
        }

        [Test]
        public void Should_create_new_daily_task()
        {
            // Setup
            var daily = CreateDaily();

            // Action
            var response = _client.CreateTaskAsync(daily);
            response.Wait();

            // Verify the result
            AssertTask(daily, response.Result);

            Assert.AreEqual(daily.Repeat.Friday, response.Result.Repeat.Friday);
            Assert.AreEqual(daily.CollapseChecklist, response.Result.CollapseChecklist);
            Assert.AreEqual(daily.Checklist.First().Id, response.Result.Checklist.First().Id);
            Assert.AreEqual(daily.Checklist.First().Text, response.Result.Checklist.First().Text);
            Assert.AreEqual(daily.Streak, response.Result.Streak);
        }

        [Test]
        public void Should_create_new_reward_task()
        {
            // Setup
            var reward = CreateReward();

            // Action
            var response = _client.CreateTaskAsync(reward);
            response.Wait();

            // Verify the result
            AssertTask(reward, response.Result);
        }

        [Test]
        public void Should_create_and_update_todo()
        {
            // Setup
            var todo = CreateTodo();

            // Action
            var response = _client.CreateTaskAsync(todo);
            response.Wait();

            AssertTask(todo, response.Result);

            var newTodo = response.Result;
            newTodo.Text = "Some new updated Text";

            response = _client.UpdateTaskAsync(newTodo);
            response.Wait();

            AssertTask(newTodo, response.Result);
        }

        [Test]
        public void Should_return_all_tasks()
        {
            // Setup
            var habitTask = CreateHabit();
            var task = _client.CreateTaskAsync(habitTask);
            task.Wait();

            // Action
            var response = _client.GetTasksAsync();
            response.Wait();

            // Verify the result
            Assert.GreaterOrEqual(response.Result.Count, 1);
        }

        [Test]
        public void Should_return_daily_task()
        {
            // Setup
            Daily daily = CreateDaily();
            var task = _client.CreateTaskAsync(daily);
            task.Wait();

            // Action
            var response = _client.GetTaskAsync<Daily>(task.Result.Id);
            response.Wait();

            // Verify the result
            AssertTask(daily, response.Result);

            Assert.AreEqual(daily.Repeat.Friday, response.Result.Repeat.Friday);
            Assert.AreEqual(daily.CollapseChecklist, response.Result.CollapseChecklist);
            Assert.AreEqual(daily.Checklist.First().Id, response.Result.Checklist.First().Id);
            Assert.AreEqual(daily.Checklist.First().Text, response.Result.Checklist.First().Text);
            Assert.AreEqual(daily.Streak, response.Result.Streak);
        }

        [Test]
        public void Should_score_existing_task()
        {
            // Setup
            var daily = CreateDaily();
            var task = _client.CreateTaskAsync(daily);
            task.Wait();

            // Action
            var response = _client.ScoreTaskAsync(task.Result.Id, Direction.Up);
            response.Wait();

            // Verify the result
            Assert.IsNotNull(response.Result);
        }

        [Test]
        public void Should_create_and_score_new_habit_task()
        {
            // Setup
            var habit = CreateHabit();

            var returnedHabit = _client.CreateTaskAsync<Habit>(habit);

            // Action
            var response = _client.ScoreTaskAsync(returnedHabit.Result.Id, Direction.Up);
            response.Wait();

            // Verify the result
            var tasks = _client.GetTasksAsync();
            tasks.Wait();

            bool exist = tasks.Result.Exists(t => t.Text.Equals(returnedHabit.Result.Text));

            Assert.IsNotNull(response);
            Assert.IsTrue(exist);
        }

        [Test]
        public void Should_get_user()
        {
            // Action
            var response = _client.GetUserAsync();
            response.Wait();

            // Verify the result
            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Result.Preferences);
        }

        [Test]
        public void Should_equip_Weapon()
        {
            var response = _client.InventoryEquip("equipped", "weapon_warrior_1");
            response.Wait();

            Assert.IsNotEmpty(response.Result.Gear.Equipped);
        }

        [Test]
        public void Should_clear_completed()
        {
            var todo = CreateTodo();

            var createTaskResponse = _client.CreateTaskAsync(todo);
            createTaskResponse.Wait();

            var scoreTaskResponse = _client.ScoreTaskAsync(createTaskResponse.Result.Id, Direction.Up);
            scoreTaskResponse.Wait();

            var clearCompletedResponse = _client.ClearCompletedAsync();
            clearCompletedResponse.Wait();
        }

        [Test]
        public void Should_get_buyable_items()
        {
            var getBuyableItemsAsyncResponse = _client.GetBuyableItemsAsync();
            getBuyableItemsAsyncResponse.Wait();

            Assert.IsNotEmpty(getBuyableItemsAsyncResponse.Result);
        }

        private static void AssertTask(TaskItem expected, TaskItem actual)
        {
            Assert.AreEqual(expected.Type, actual.Type);
            AssertDateTime(expected.DateCreated.Value, actual.DateCreated.Value);
            Assert.AreEqual(expected.Text, actual.Text);
            Assert.AreEqual(expected.Notes, actual.Notes);
            Assert.AreEqual(expected.Tags.First().GetHashCode(), actual.Tags.First().GetHashCode());
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.Priority, actual.Priority);
            Assert.AreEqual(expected.Attribute, actual.Attribute);

            if (expected.Challenge != null)
            {
                Assert.AreEqual(expected.Challenge.Id, actual.Challenge.Id);
            }
        }

        private static void AssertDateTime(DateTime expected, DateTime actual)
        {
            Assert.AreEqual(Math.Abs((expected - actual).TotalSeconds) < 5, true);
        }
        */
        private static Daily CreateDaily()
        {
            var daily = new Daily
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Attribute.Strength,
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
        
        private static Habit CreateHabit()
        {
            var habitTask = new Habit
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Attribute.Strength,
                Down = false
            };

            return habitTask;
        }
        
        private static Todo CreateTodo()
        {
            var habitTask = new Todo
            {
                Text = "Main Task: " + DateTime.UtcNow,
                Notes = "Notes",
                Value = 0,
                Priority = Difficulty.Hard,
                Attribute = Attribute.Strength,
                Completed = true,
                Archived = true,
                DateCompleted = DateTime.Now,
                DueDate = DateTime.Now,
                CollapseChecklist = true
            };

            return habitTask;
        }/*

        private static Reward CreateReward()
        {
            var reward = new Reward
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.UtcNow,
                Text = "Main Task: " + DateTime.Now,
                Notes = "Notes",
                TagGuids = new List<Guid>
            {
               Guid.NewGuid()
            },
                Value = 1110,
                Priority = Difficulty.Hard,
                Attribute = Attribute.Strength
            };

            return reward;
        }*/
    }
}