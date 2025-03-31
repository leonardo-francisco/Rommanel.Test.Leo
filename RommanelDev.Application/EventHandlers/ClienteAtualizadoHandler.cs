using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.EventHandlers
{
    public class ClienteAtualizadoHandler : INotificationHandler<ClienteAtualizadoEvent>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAtualizadoHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Handle(ClienteAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(notification.ClienteId);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            cliente.Nome = notification.Nome;
            cliente.Telefone = notification.Telefone;
            cliente.Email = notification.Email;
            cliente.IsentoIE = notification.IsentoIE;

            await _clienteRepository.UpdateAsync(cliente);
        }
    }
}
