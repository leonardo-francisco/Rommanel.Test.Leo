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
    public class ClientUpdatedHandler : INotificationHandler<ClientUpdatedEvent>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientUpdatedHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task Handle(ClientUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Client>(notification);
            await _clientRepository.UpdateAsync(cliente);
        }
    }
}
