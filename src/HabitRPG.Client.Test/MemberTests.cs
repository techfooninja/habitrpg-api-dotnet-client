using HabitRPG.Client.Model;
using NUnit.Framework;
using System;

namespace HabitRPG.Client.Test
{
    [TestFixture]
    public class MemberTests : IntegrationBase
    {
        public MemberTests()
        {
        }

        [Test]
        public void GetMember()
        {
            var response = Member.GetAsync(Guid.Parse("d28ceae9-f20f-49a4-9219-3bde6a887288"));
            response.Wait();

            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Result.Preferences);
        }

        [Test]
        public void GetLoggedInUser()
        {
            var response = User.GetAsync();
            response.Wait();

            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Result.Preferences);
        }
    }
}
