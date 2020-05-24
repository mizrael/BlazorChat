using System;

namespace BlazorChat.Services
{

    public class ChatService : IChatService
    {
        public ChatService(IUserStateProvider provider)
        {
            _provider = provider;
        }

        private readonly IUserStateProvider _provider;

        public UserState Login(string username, ConnectedClient client)
        {
            var user = new UserState(Guid.NewGuid(), username, client, DateTime.UtcNow);
            _provider.AddOrUpdate(user);
            this.UserLoggedIn?.Invoke(this, new UserLoginEventArgs(user));
            return user;
        }

        public event EventHandler<UserLoginEventArgs> UserLoggedIn;

        public void Logout(Guid userId)
        {
            _provider.Remove(userId);
            this.UserLoggedOut?.Invoke(this, new UserLogoutEventArgs(userId));
        }

        public event EventHandler<UserLogoutEventArgs> UserLoggedOut;

        public void PostMessage(Guid userId, string message) { 

        }


    }
}
