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
        public void GetAllTasks()
        {
            var daily = CreateDaily();
            var taskResponse = daily.SaveAsync();
            taskResponse.Wait();

            var response = TaskItem.GetAllAsync();
            response.Wait();

            Assert.AreNotEqual(0, response.Result.Count);
        }

        [Test]
        public void GetAllTasksWithFilter()
        {
            var daily = CreateDaily();
            var taskResponse = daily.SaveAsync();
            taskResponse.Wait();

            var response = TaskItem.GetAllAsync(TaskQuery.Dailies);
            response.Wait();

            Assert.AreNotEqual(0, response.Result.Count);
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

        [Test]
        public void CreateNewReward()
        {
            var reward = CreateReward();
            var response = reward.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, reward.Id);
        }

        [Test]
        public void DeleteTodo()
        {
            var task = CreateTodo();
            var response = task.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, task.Id);

            response = task.DeleteAsync();
            response.Wait();

            try
            {
                response = TaskItem.GetAsync(task.Id);
                response.Wait();

                Assert.Fail("Should not be able to delete a task that is already deleted");
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DeleteHabit()
        {
            var task = CreateHabit();
            var response = task.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, task.Id);

            response = task.DeleteAsync();
            response.Wait();

            try
            {
                response = TaskItem.GetAsync(task.Id);
                response.Wait();

                Assert.Fail("Should not be able to delete a task that is already deleted");
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DeleteDaily()
        {
            var task = CreateDaily();
            var response = task.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, task.Id);

            response = task.DeleteAsync();
            response.Wait();

            try
            {
                response = TaskItem.GetAsync(task.Id);
                response.Wait();

                Assert.Fail("Should not be able to delete a task that is already deleted");
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DeleteReward()
        {
            var task = CreateReward();
            var response = task.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, task.Id);

            response = task.DeleteAsync();
            response.Wait();

            try
            {
                response = TaskItem.GetAsync(task.Id);
                response.Wait();

                Assert.Fail("Should not be able to delete a task that is already deleted");
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateTask()
        {
            var task = CreateDaily();
            var response = task.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, task.Id);

            task.Text = "Updated task text";
            task.Streak = 100;
            response = task.SaveAsync();
            response.Wait();

            Assert.AreEqual("Updated task text", task.Text);
            Assert.AreEqual(100, task.Streak);
        }
        
        [Test]
        public void GetAllTags()
        {
            var response = TaskItem.GetTagsAsync();
            response.Wait();

            Assert.AreNotEqual(0, response.Result.Count);
        }

        [Test]
        public void AssignTagToTask()
        {
            var response = TaskItem.GetTagsAsync();
            response.Wait();

            Assert.AreNotEqual(0, response.Result.Count);

            var habit = CreateHabit();
            var addResponse = habit.AddTagAsync(response.Result[0]);
            addResponse.Wait();

            Assert.AreNotEqual(0, habit.Tags.Count);
            Assert.AreEqual(response.Result[0], habit.Tags.First());
        }

        [Test]
        public void DeleteTagFromTask()
        {
            var response = TaskItem.GetTagsAsync();
            response.Wait();

            Assert.AreNotEqual(0, response.Result.Count);

            var habit = CreateHabit();
            var addResponse = habit.AddTagAsync(response.Result[0]);
            addResponse.Wait();

            Assert.AreNotEqual(0, habit.Tags.Count);
            Assert.AreEqual(response.Result[0], habit.Tags.First());

            var deleteResponse = habit.DeleteTagAsync(response.Result[0]);
            deleteResponse.Wait();

            Assert.AreEqual(0, habit.Tags.Count);
        }

        [Test]
        public void ScoreDaily()
        {
            var daily = CreateDaily();
            var response = daily.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, daily.Id);

            response = daily.ScoreAsync();
            response.Wait();

            Assert.IsTrue(daily.Completed);
        }

        [Test]
        public void ScoreHabitUp()
        {
            var habit = CreateHabit();
            var response = habit.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, habit.Id);

            int oldCount = habit.CounterUp;

            response = habit.ScoreAsync();
            response.Wait();

            Assert.AreEqual(oldCount + 1, habit.CounterUp);
        }

        [Test]
        public void ScoreHabitDown()
        {
            var habit = CreateHabit();
            var response = habit.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, habit.Id);

            int oldCount = habit.CounterDown;

            response = habit.ScoreAsync(Direction.Down);
            response.Wait();

            Assert.AreEqual(oldCount + 1, habit.CounterDown);
        }

        [Test]
        public void ScoreTodo()
        {
            var todo = CreateTodo();
            var response = todo.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, todo.Id);

            response = todo.ScoreAsync();
            response.Wait();

            Assert.IsTrue(todo.Completed);
        }

        [Test]
        public void ScoreReward()
        {
            var reward = CreateReward();
            var response = reward.SaveAsync();
            response.Wait();

            Assert.AreNotEqual(Guid.Empty, reward.Id);

            var scoreResponse = reward.ScoreAsync();
            scoreResponse.Wait();
        }

        [Test]
        public void AddChecklistItem()
        {
            var daily = CreateDaily();
            var response = daily.AddChecklistItemAsync(new Checklist() { Text = "Checklist item 1" });
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.AreEqual("Checklist item 1", daily.Checklist[0].Text);
        }

        [Test]
        public void DeleteChecklistItem()
        {
            var daily = CreateDaily();
            var response = daily.AddChecklistItemAsync(new Checklist() { Text = "Checklist item 1" });
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.AreEqual("Checklist item 1", daily.Checklist[0].Text);

            response = daily.DeleteChecklistItemAsync(daily.Checklist[0]);
            response.Wait();

            Assert.AreEqual(0, daily.Checklist.Count);
        }

        [Test]
        public void UpdateChecklistItem()
        {
            var daily = CreateDaily();
            var response = daily.AddChecklistItemAsync(new Checklist() { Text = "Checklist item 1" });
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.AreEqual("Checklist item 1", daily.Checklist[0].Text);

            daily.Checklist[0].Text = "New Checklist item 2";
            response = daily.UpdateChecklistItem(daily.Checklist[0]);
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.AreEqual("New Checklist item 2", daily.Checklist[0].Text);
        }

        [Test]
        public void ScoreChecklistItem()
        {
            var daily = CreateDaily();
            var response = daily.AddChecklistItemAsync(new Checklist() { Text = "Checklist item 1" });
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.AreEqual("Checklist item 1", daily.Checklist[0].Text);
            
            response = daily.ScoreChecklistItemAsync(daily.Checklist[0]);
            response.Wait();

            Assert.AreEqual(1, daily.Checklist.Count);
            Assert.IsTrue(daily.Checklist[0].Completed);
        }

        [Test]
        public void ClearCompletedTodos()
        {
            var todo = CreateTodo();
            var saveResponse = todo.SaveAsync();
            saveResponse.Wait();

            var response = todo.ScoreAsync();
            response.Wait();

            var clearResponse = TaskItem.ClearCompletedTodos();
            clearResponse.Wait();
        }

        /*

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
        public void Should_get_buyable_items()
        {
            var getBuyableItemsAsyncResponse = _client.GetBuyableItemsAsync();
            getBuyableItemsAsyncResponse.Wait();

            Assert.IsNotEmpty(getBuyableItemsAsyncResponse.Result);
        }
        */
    }
}