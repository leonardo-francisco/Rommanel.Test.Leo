using AutoMapper;
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
    public class RemoveClienteHandler : IRequestHandler<RemoveClienteCommand, bool>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClienteRepository _clienteRepository;

        public RemoveClienteHandler(IEventStore eventStore, IMapper mapper, IMediator mediator, IClienteRepository clienteRepository)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _mediator = mediator;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(RemoveClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente is null)
                return false;

            // Cria um evento de remoção
            var clienteRemovidoEvent = new ClienteRemovidoEvent(cliente.Id.ToString());

            // Salva o evento de remoção
            await _eventStore.SaveAsync(clienteRemovidoEvent);

            // Publica o evento de remoção
            await _mediator.Publish(clienteRemovidoEvent, cancellationToken);

            return true;
        }
    }
}
