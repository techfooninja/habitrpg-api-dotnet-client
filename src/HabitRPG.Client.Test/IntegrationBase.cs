﻿using System;
using NUnit.Framework;
using HabitRPG.Client.Model;

namespace HabitRPG.Client.Test
{
    [TestFixture]
    public class IntegrationBase
    {
        private readonly HabiticaClient _client;

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
    }
}
