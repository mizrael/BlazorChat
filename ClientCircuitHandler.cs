using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorChat.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BlazorChat
{
    public class ClientCircuitHandler : CircuitHandler
    {
        private Services.IConnectedClientProvider _provider;
        private Services.IUserStateProvider _usersProvider;
        private Services.IChatService _chatService;

        public ClientCircuitHandler(IConnectedClientProvider provider, IChatService chatService, IUserStateProvider usersProvider)
        {
            _provider = provider;
            _chatService = chatService;
            _usersProvider = usersProvider;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _provider.Connect(circuit.Id);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            var user = _usersProvider.GetByClient(_provider.Client);
            if(null != user)
                _chatService.Logout(user.Username);
            
            _provider.Disconnect();
            
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }
}
