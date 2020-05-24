using System;

namespace BlazorChat.Services
{
    public class UserLogoutEventArgs : EventArgs
    {
        public Guid UserId { get; }

        public UserLogoutEventArgs(Guid userId)
        {
            UserId = userId;
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string Username { get; }
        public string Message { get; }
    }

}
