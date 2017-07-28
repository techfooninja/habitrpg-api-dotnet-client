namespace HabitRPG.Client.Model
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
    using System.Reflection;

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

        protected virtual void CopyFrom(object obj)
        {
            // No implementation required
        }
        
        protected virtual void UpdateCollection<T>(Dictionary<Guid, T> original, Dictionary<Guid, T> amend, bool deleteStaleEntries = true) where T : HabiticaObject
        {
            if (deleteStaleEntries)
            {
                foreach (var kvp in original)
                {
                    // If an item exists in original, but not in amend, then it should be removed from original
                    var amendObject = amend[kvp.Key];
                    if (amendObject == null)
                    {
                        original.Remove(kvp.Key);
                    }
                }
            }

            foreach (var kvp in amend)
            {
                var originalObject = original[kvp.Key];
                if (originalObject == null)
                {
                    // If it doesn't exist in original yet, add it
                    original.Add(kvp.Key, kvp.Value);
                }
                else
                {
                    // Otherwise, update the existing item
                    originalObject.CopyFrom(kvp.Value);
                }
            }
        }

        protected virtual void UpdateCollection<T>(List<T> original, List<T> amend, bool deleteStaleEntries = true) where T : HabiticaObject
        {
            if (deleteStaleEntries)
            {
                original.Clear();
            }

            original.AddRange(amend);
        }
    }
}
