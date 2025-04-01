using AutoMapper;
using MediatR;
using RommanelDev._Domain.Aggregates;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using RommanelDev.Application.DTO;
using RommanelDev.Infrastructure.EventStore;
using RommanelDev.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands.Handler
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClientRepository _clienteRepository;

        public UpdateClienteHandler(IEventStore eventStore, IMapper mapper, IMediator mediator, IClientRepository clienteRepository)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _mediator = mediator;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
            {
                return false;
            }

            var endereco = AddressDto.FromDto(request.Address);
            var clienteAggregate = ClientAggregate.FromClient(cliente);

            clienteAggregate.Update(cliente.Id.ToString(),
                                     request.Name,
                                     request.BirthDate,
                                     request.Phone,
                                     endereco,
                                     request.FreeIE
                                 );

            foreach (var @event in clienteAggregate.GetUncommittedEvents())
            {
                await _eventStore.SaveAsync(@event); 
                await _mediator.Publish(@event, cancellationToken); 
            }


            clienteAggregate.ClearUncommittedEvents();

            return true;
        }
    }
}
