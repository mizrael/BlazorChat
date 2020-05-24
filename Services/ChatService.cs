using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public UserState Login(string username, ConnectedClient client)
        {
            var user = new UserState(Guid.NewGuid(), username, client, DateTime.UtcNow);
            _usersProvider.AddOrUpdate(user);
            this.UserLoggedIn?.Invoke(this, new UserLoginEventArgs(user));
            return user;
        }

        public IEnumerable<UserState> GetAllUsers() => _usersProvider.GetAll();

        public event EventHandler<UserLoginEventArgs> UserLoggedIn;

        public void Logout(Guid userId)
        {
            _usersProvider.Remove(userId);
            this.UserLoggedOut?.Invoke(this, new UserLogoutEventArgs(userId));
        }

        public event EventHandler<UserLogoutEventArgs> UserLoggedOut;

        public async Task PostMessageAsync(UserState user, string message)
        {
            await _publisher.PublishAsync(new Message(user.Username, message, DateTime.UtcNow));
        }
    }
}
