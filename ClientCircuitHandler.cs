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

        public ClientCircuitHandler(IConnectedClientProvider provider)
        {
            _provider = provider;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _provider.Connect(circuit.Id);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _provider.Disconnect();
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }
}
