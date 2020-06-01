using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorChat.Models;
using BlazorChat.Providers;

namespace BlazorChat.Services
{

    public class ChatService : IChatService
    {
        private readonly IUserStateProvider _usersProvider;
        private readonly IMessagesPublisher _publisher;
        private readonly IMessagesConsumer _consumer;

        public ChatService(IUserStateProvider usersProvider, IMessagesPublisher publisher, IMessagesConsumer consumer)
        {
            _usersProvider = usersProvider;
            _publisher = publisher;
            _consumer = consumer;
            _consumer.MessageReceived += OnMessage;
        }

        private void OnMessage(object sender, Message message)
        {
            this.MessageReceived?.Invoke(this, message);
        }

        public event EventHandler<Message> MessageReceived;

        public User Login(string username, ConnectedClient client)
        {
            var user = new User(username);
            user.Connect(client);
            _usersProvider.AddOrUpdate(user);
            this.UserLoggedIn?.Invoke(this, new UserLoginEventArgs(user));
            return user;
        }

        public IEnumerable<User> GetAllUsers() => _usersProvider.GetAll();

        public event EventHandler<UserLoginEventArgs> UserLoggedIn;

        public void Logout(string username)
        {
            var user = _usersProvider.GetByUsername(username);
            if (null != user)
            {
                user.Disconnect();
                _usersProvider.AddOrUpdate(user);
            }
            
            this.UserLoggedOut?.Invoke(this, new UserLogoutEventArgs(username));
        }

        public event EventHandler<UserLogoutEventArgs> UserLoggedOut;

        public async Task PostMessageAsync(User user, string message)
        {
            await _publisher.PublishAsync(new Message(user.Username, message, DateTime.UtcNow));
        }
    }
}
