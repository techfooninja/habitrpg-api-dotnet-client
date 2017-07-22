using HabitRPG.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HabitRPG.Client.Extensions;
using HabitRPG.Client.Common;

namespace HabitRPG.Client.Clients
{
    public class LoginClient : ClientBase, ILoginClient
    {
        public LoginClient(HabitRpgConfiguration habitRpgConfiguration) : base(habitRpgConfiguration)
        {
        }

        public LoginClient(HabitRpgConfiguration habitRpgConfiguration, IWebProxy httpClient) : base(habitRpgConfiguration, httpClient)
        {
        }

        public LoginClient(HabitRpgConfiguration habitRpgConfiguration, HttpClientHandler httpClientHandler) : base(habitRpgConfiguration, httpClientHandler)
        {
        }

        public LoginClient(Uri serviceUri) : base(Guid.Empty, Guid.Empty, serviceUri)
        {
        }

        public LoginClient(HabitRpgConfiguration habitRpgConfiguration, HttpClient httpClient) : base(habitRpgConfiguration, httpClient)
        {
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            return await LoginAsync(new LoginCredential() { Username = username, Password = password });
        }

        public async Task<LoginResult> LoginAsync(LoginCredential cred)
        {
            if (cred == null)
            {
                throw new ArgumentNullException("cred");
            }

            var response = await HttpClient.PostAsJsonAsync("user/auth/local/login", cred);

            return GetResult<LoginResult>(response);
        }
    }
}
