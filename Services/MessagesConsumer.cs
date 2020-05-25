using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using BlazorChat.Models;

namespace BlazorChat.Services
{
    public interface IMessagesConsumer
    {
        Task BeginConsumeAsync();
        event EventHandler<Message> MessageReceived;
    }

    public class MessagesConsumer : IMessagesConsumer
    {
        private readonly ChannelReader<Message> _reader;

        public MessagesConsumer(ChannelReader<Message> reader)
        {
            _reader = reader;
        }

        public async Task BeginConsumeAsync()
        {
            await foreach (var message in _reader.ReadAllAsync())
            {
                this.MessageReceived?.Invoke(this, message);
            }
        }

        public event EventHandler<Message> MessageReceived;
    }
}
