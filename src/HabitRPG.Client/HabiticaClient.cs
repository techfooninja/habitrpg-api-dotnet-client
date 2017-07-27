namespace HabitRPG.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;
    using HabitRPG.Client.Model;
    using HabitRPG.Client.Extensions;
    using System.Diagnostics;

    public class HabiticaClient : HabiticaObject, IDisposable
    {
        #region Constructors

        public HabiticaClient(HabitRpgConfiguration habitRpgConfiguration)
          : this(habitRpgConfiguration, new HttpClient(new HttpClientHandler()))
        {
        }

        public HabiticaClient(HabitRpgConfiguration habitRpgConfiguration, IWebProxy httpClient)
          : this(habitRpgConfiguration, new HttpClientHandler { Proxy = httpClient, UseProxy = true })
        {
        }

        public HabiticaClient(HabitRpgConfiguration habitRpgConfiguration, HttpClientHandler httpClientHandler)
          : this(habitRpgConfiguration, new HttpClient(httpClientHandler))
        {
        }

        public HabiticaClient(Guid userId, Guid apiToken, Uri serviceUri)
          : this(new HabitRpgConfiguration { ApiToken = apiToken, ServiceUri = serviceUri, UserId = userId }, new HttpClient())
        {
        }

        public HabiticaClient(HabitRpgConfiguration habitRpgConfiguration, HttpClient httpClient)
        {
            if (habitRpgConfiguration == null)
            {
                throw new ArgumentNullException("habitRpgConfiguration");
            }

            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }

            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri(habitRpgConfiguration.ServiceUri, "api/v3/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("x-api-user", habitRpgConfiguration.UserId.ToString());
            HttpClient.DefaultRequestHeaders.Add("x-api-key", habitRpgConfiguration.ApiToken.ToString());

            Configuration = habitRpgConfiguration;
        }

        #endregion Constructors

        #region Dispose

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    HttpClient.Dispose();
                }

                _disposed = true;
            }
        }

        ~HabiticaClient()
        {
            Dispose(false);
        }

        #endregion Dispose

        #region User APIs

        public async Task<Items> InventoryEquip(string type, string key)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            var response = await HttpClient.PostAsync(String.Format("user/equip/{0}/{1}", type, key), null);

            response.EnsureSuccessStatusCode();

            return GetResult<Items>(response);
        }

        public async Task<User> GetUserAsync()
        {
            var response = await HttpClient.GetAsync("user");

            return GetResult<User>(response);
        }

        public async Task<List<Item>> GetBuyableItemsAsync()
        {
            var response = await HttpClient.GetAsync("user/inventory/buy");

            return GetResult<List<Item>>(response);
        }

        public async Task BuyItemAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            var response = await HttpClient.PostAsync(String.Format("user/inventory/buy/{0}", key), null);

            response.EnsureSuccessStatusCode();
        }

        public async Task SetGroupChatAsSeenAsync(string groupId)
        {
            await HttpClient.PostAsync(String.Format("groups/{0}/chat/seen", groupId), null);
        }

        public async Task ClearCompletedAsync()
        {
            await HttpClient.PostAsync("tasks/clearCompletedTodos", null);
        }

        #endregion User APIs

        #region Member APIs

        public async Task<Member> GetMemberAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            var response = await HttpClient.GetAsync(string.Format("members/{0}", id));

            return GetResult<Member>(response);
        }

        #endregion Member APIs

        #region Group APIs

        public async Task<List<Group>> GetGroupsAsync(string types)
        {
            if (types == null)
            {
                throw new ArgumentNullException("types");
            }

            var response = await HttpClient.GetAsync(String.Format("groups?type={0}", types));

            return GetResult<List<Group>>(response);
        }

        public async Task<Group> GetGroupAsync(string groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException("groupId");
            }

            var response = await HttpClient.GetAsync(String.Format("groups/{0}", groupId));

            return GetResult<Group>(response);
        }

        public async Task<List<ChatMessage>> GetGroupChatAsync(string groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException("groupId");
            }

            var response = await HttpClient.GetAsync(String.Format("groups/{0}/chat", groupId));

            return GetResult<List<ChatMessage>>(response);
        }

        public async Task<ChatMessage> SendChatMessageAsync(string groupId, string message)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException("groupId");
            }

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var response = await HttpClient.PostAsync(String.Format("groups/{0}/chat?message={1}", groupId, message), null);

            return GetResult<ChatMessage>(response);
        }

        public async Task DeleteChatMessageAsync(string groupId, string messageId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException("groupId");
            }

            if (messageId == null)
            {
                throw new ArgumentNullException("messageId");
            }

            await HttpClient.DeleteAsync(String.Format("groups/{0}/chat/{1}/like", groupId, messageId));
        }

        public async Task LikeChatMessageAsync(string groupId, string messageId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException("groupId");
            }

            if (messageId == null)
            {
                throw new ArgumentNullException("messageId");
            }

            await HttpClient.PostAsync(String.Format("groups/{0}/chat/{1}/like", groupId, messageId), null);
        }

        #endregion Group APIs

        #region Content APIs

        public async Task<Content> GetContentAsync(string language = "")
        {
            var response = await HttpClient.GetAsync(String.Format("content?language={0}", language));

            return GetResult<Content>(response);
        }

        #endregion Content APIs

        #region Status APIs

        public async Task<ServerStatus> GetStatusAsync()
        {
            var response = await HttpClient.GetAsync("status");

            return GetResult<ServerStatus>(response);
        }

        #endregion Status APIs
    }
}
