using AutoMapper;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.EventHandlers
{
    public class ClienteCriadoHandler : INotificationHandler<ClienteCriadoEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteCriadoHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task Handle(ClienteCriadoEvent notification, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Cliente>(notification);
            await _clienteRepository.AddAsync(cliente);
        }
    }
}
