using System;

namespace BlazorChat.Models
{
    public class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; }
        public ConnectedClient Client { get; private set; }
        public void Connect(ConnectedClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void Disconnect()
        {
            this.Client = null;
        }
    }
}
