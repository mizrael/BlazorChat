using System;

namespace BlazorChat.Services
{
    public class UserState
    {
        public UserState(string username, ConnectedClient clientId)
        {
            Username = username;
            Client = clientId;
        }
        public string Username { get; }
        public ConnectedClient Client { get; }
        public bool IsOnline {get;set;}
    }
}
