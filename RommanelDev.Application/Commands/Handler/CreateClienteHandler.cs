using AutoMapper;
using MediatR;
using RommanelDev._Domain.Aggregates;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using RommanelDev.Application.DTO;
using RommanelDev.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands.Handler
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, string>
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateClienteHandler(IEventStore eventStore, IMediator mediator, IMapper mapper)
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _mapper = mapper;

        }

        public async Task<string> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var endereco = EnderecoDto.FromDto(request.Endereco);

            var cliente = new ClienteAggregate(
            Guid.NewGuid().ToString(),
            request.Nome,
            request.Cpf,
            request.Cnpj,
            request.DataNascimento,
            request.Telefone,
            request.Email,
            endereco,
            request.IsentoIE
                   );

            foreach (var @event in cliente.GetUncommittedEvents())
            {
                await _eventStore.SaveAsync(@event);
                await _mediator.Publish(@event);
            }

            return cliente.Id;
        }
    }
}
