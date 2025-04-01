using AutoMapper;
using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Events;
using RommanelDev.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands.Handler
{
    public class RemoveClienteHandler : IRequestHandler<RemoveClientCommand, bool>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClientRepository _clienteRepository;

        public RemoveClienteHandler(IEventStore eventStore, IMapper mapper, IMediator mediator, IClientRepository clienteRepository)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _mediator = mediator;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente is null)
                return false;
           
            var clienteRemovidoEvent = new ClientRemovedEvent(cliente.Id.ToString());

            await _eventStore.SaveAsync(clienteRemovidoEvent);

            await _mediator.Publish(clienteRemovidoEvent, cancellationToken);

            return true;
        }
    }
}
