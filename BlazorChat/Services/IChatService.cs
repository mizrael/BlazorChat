using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorChat.Models;

namespace BlazorChat.Services
{
    public interface IChatService
    {
        event EventHandler<UserLoginEventArgs> UserLoggedIn;
        event EventHandler<UserLogoutEventArgs> UserLoggedOut;
        event EventHandler<Message> MessageReceived;

        User Login(string username, ConnectedClient client);
        IEnumerable<User> GetAllUsers();
        void Logout(string username);
        Task PostMessageAsync(User user, string message);
    }
}
