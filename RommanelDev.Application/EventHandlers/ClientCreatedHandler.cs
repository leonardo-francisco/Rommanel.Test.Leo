using AutoMapper;
using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.Events;
using RommanelDev.Application.Commands.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.EventHandlers
{
    public class ClientCreatedHandler : INotificationHandler<ClientCreatedEvent>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientCreatedHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Client>(notification);
            var clienteCadastrado = await _clientRepository.AddAsync(cliente);
            CreateClienteHandler.SetClienteId(clienteCadastrado.Id.ToString());
        }
    }
}
