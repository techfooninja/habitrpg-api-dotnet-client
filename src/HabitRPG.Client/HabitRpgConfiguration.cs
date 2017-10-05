using System;
using System.Collections.Generic;
using HabitRPG.Client.Converters;
using Newtonsoft.Json;

namespace HabitRPG.Client
{
    public class HabitRpgConfiguration
    {
        public HabitRpgConfiguration()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new TaskConverter(),
                    new ChallengeConverter(),
                    new TimestampConverter(),
                },

#if DEBUG
                Error = (sender, args) =>
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }
#endif
            };
        }

        public Guid ApiToken { get; set; }

        public Guid UserId { get; set; }

        public Uri ServiceUri { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is HabitRpgConfiguration)
            {
                HabitRpgConfiguration config = (HabitRpgConfiguration)obj;
                return
                    ApiToken.Equals(config.ApiToken) &&
                    UserId.Equals(config.UserId) &&
                    ServiceUri.Equals(config.ServiceUri);
            }

            return false;
        }
    }
}