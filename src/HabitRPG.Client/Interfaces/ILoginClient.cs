using System.Threading.Tasks;
using HabitRPG.Client.Model;

namespace HabitRPG.Client
{
    public interface ILoginClient
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task<LoginResult> LoginAsync(LoginCredential cred);
    }
}