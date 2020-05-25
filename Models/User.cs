using System;

namespace BlazorChat.Models
{
    public class User
    {
        public User(string username, ConnectedClient clientId)
        {
            Username = username;
            Client = clientId; 
        }
        public string Username { get; }
        public ConnectedClient Client { get; }
        public bool IsOnline {get;set;}
    }
}
