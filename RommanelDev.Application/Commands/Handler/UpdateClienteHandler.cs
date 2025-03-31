using AutoMapper;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using RommanelDev.Infrastructure.EventStore;
using RommanelDev.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands.Handler
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, bool>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClienteRepository _clienteRepository;

        public UpdateClienteHandler(IEventStore eventStore, IMapper mapper, IMediator mediator, IClienteRepository clienteRepository)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _mediator = mediator;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
            {
                return false;
            }

            // Atualiza os dados do cliente (Você pode ter um método como AtualizarDados no Aggregate)
            cliente.AtualizarDados(request.Nome, request.Telefone, request.Email, request.IsentoIE);

            // Salva os eventos de atualização
            foreach (var @event in cliente.GetUncommittedEvents())
            {
                await _eventStore.SaveAsync(@event); // Armazena os eventos
                await _mediator.Publish(@event, cancellationToken); // Publica os eventos
            }

            return true;
        }
    }
}
