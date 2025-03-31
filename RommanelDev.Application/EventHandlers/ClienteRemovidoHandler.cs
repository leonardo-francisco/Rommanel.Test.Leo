using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.EventHandlers
{
    public class ClienteRemovidoHandler : INotificationHandler<ClienteRemovidoEvent>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteRemovidoHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Handle(ClienteRemovidoEvent notification, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(notification.ClienteId);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            await _clienteRepository.RemoveAsync(cliente.Id.ToString());
        }
    }
}
