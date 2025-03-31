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

            var endereco = EnderecoDto.FromDto(request.Endereco);
            var clienteAggregate = ClienteAggregate.FromCliente(cliente);

            clienteAggregate.Atualizar(cliente.Id.ToString(),
                                     request.Nome,
                                     request.DataNascimento,
                                     request.Telefone,
                                     endereco,
                                     request.IsentoIE
                                 );

            foreach (var @event in clienteAggregate.GetUncommittedEvents())
            {
                await _eventStore.SaveAsync(@event); // Armazena os eventos
                await _mediator.Publish(@event, cancellationToken); // Publica os eventos
            }


            clienteAggregate.ClearUncommittedEvents();

            return true;
        }
    }
}
