namespace BlazorChat.Services
{
    public interface IConnectedClientProvider
    {
        ConnectedClient Client { get; }

        void Connect(string id);
        void Disconnect();
    }

    public class ConnectedClientProvider : IConnectedClientProvider
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


    public class ConnectedClient
    {
        public string Id { get; }

        public ConnectedClient(string id)
        {
            Id = id;
        }
    }

}
