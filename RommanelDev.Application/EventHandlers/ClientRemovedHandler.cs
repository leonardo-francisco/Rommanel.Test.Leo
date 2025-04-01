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
    public class ClientRemovedHandler : INotificationHandler<ClientRemovedEvent>
    {
        private readonly IClientRepository _clientRepository;

        public ClientRemovedHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(ClientRemovedEvent notification, CancellationToken cancellationToken)
        {
            var cliente = await _clientRepository.GetByIdAsync(notification.ClientId);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            await _clientRepository.RemoveAsync(cliente.Id.ToString());
        }
    }
}
