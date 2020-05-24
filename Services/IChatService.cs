using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.Services
{
    public interface IChatService
    {
        event EventHandler<UserLoginEventArgs> UserLoggedIn;
        event EventHandler<UserLogoutEventArgs> UserLoggedOut;
        event EventHandler<Message> MessageReceived;

        UserState Login(string username, ConnectedClient client);
        IEnumerable<UserState> GetAllUsers();
        void Logout(Guid userId);
        Task PostMessageAsync(UserState user, string message);
    }
}
