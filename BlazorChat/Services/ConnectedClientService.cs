using BlazorChat.Models;

namespace BlazorChat.Services
{
    public interface IConnectedClientService
    {
        ConnectedClient Client { get; }

        void Connect(string id);
        void Disconnect();
    }

    public class InMemoryConnectedClientService : IConnectedClientService
    {
        public ConnectedClient Client { get; private set; }

        public void Connect(string id)
        {
            this.Client = new ConnectedClient(id);
        }

        public void Disconnect()
        {
            this.Client = null;
        }
    }

}
