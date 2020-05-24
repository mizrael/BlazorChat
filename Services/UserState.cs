using System;

namespace BlazorChat.Services
{
    public class UserState
    {
        public UserState(Guid id, string username, ConnectedClient clientId, DateTime loginTime)
        {
            Id = id;
            Username = username;
            Client = clientId;
            LoginTime = loginTime;
        }

        public Guid Id { get; }
        public string Username { get; }
        public ConnectedClient Client { get; }
        public DateTime LoginTime { get; }
    }
}
