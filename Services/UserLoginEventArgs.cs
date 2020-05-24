using System;

namespace BlazorChat.Services
{
    public class UserLoginEventArgs : EventArgs
    {
        public UserState User { get; }

        public UserLoginEventArgs(UserState user)
        {
            User = user;
        }
    }

}
