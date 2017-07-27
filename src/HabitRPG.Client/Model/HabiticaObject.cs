﻿namespace HabitRPG.Client.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using HabitRPG.Client.Extensions;

    public class HabiticaObject
    {
        protected static HttpClient HttpClient { get; set; }

        protected static HabitRpgConfiguration Configuration { get; set; }

        protected static T GetResult<T>(HttpResponseMessage response)
        {
            Debug.WriteLine("URL: {0} Method: {1} StatusCode: {2}", response.RequestMessage.RequestUri,
                response.RequestMessage.Method, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var contentJson = response.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Result: {0} ", contentJson);

            var deserializeObject = JsonConvert.DeserializeObject<ApiResponse<T>>(contentJson, Configuration.SerializerSettings);

            return deserializeObject.Data;
        }
    }
}