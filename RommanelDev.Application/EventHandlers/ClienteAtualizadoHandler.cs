using AutoMapper;
using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.Events;
using RommanelDev._Domain.ValueObjects;
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
        private readonly IMapper _mapper;

        public ClienteAtualizadoHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task Handle(ClienteAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Cliente>(notification);
            await _clienteRepository.UpdateAsync(cliente);
        }
    }
}
