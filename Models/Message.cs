using System;

namespace BlazorChat.Models
{
    public class Message
    {
        public Message(string username, string text, DateTime when)
        {
            Username = username;
            Text = text;
            When = when;
        }
        public string Username { get; }
        public string Text { get; }
        public DateTime When { get; }
    }
}
