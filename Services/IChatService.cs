using System;

namespace BlazorChat.Services
{
    public interface IChatService
    {
        event EventHandler<UserLoginEventArgs> UserLoggedIn;
        event EventHandler<UserLogoutEventArgs> UserLoggedOut;

        UserState Login(string username, ConnectedClient client);
        void Logout(Guid userId);
        void PostMessage(Guid userId, string message);
    }

}
