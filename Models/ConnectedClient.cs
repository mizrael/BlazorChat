namespace BlazorChat.Models
{
    public class ConnectedClient
    {
        public string Id { get; }

        public ConnectedClient(string id)
        {
            Id = id;
        }
    }

}
