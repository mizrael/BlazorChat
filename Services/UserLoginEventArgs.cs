using System;
using BlazorChat.Models;

namespace BlazorChat.Services
{
    public class UserLoginEventArgs : EventArgs
    {
        public User User { get; }

        public UserLoginEventArgs(User user)
        {
            User = user;
        }
    }

}
