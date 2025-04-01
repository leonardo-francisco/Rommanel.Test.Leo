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
    public class CreateClienteHandler : IRequestHandler<CreateClientCommand, string>
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private static readonly TaskCompletionSource<string> _idSource = new TaskCompletionSource<string>();

        public CreateClienteHandler(IEventStore eventStore, IMediator mediator, IMapper mapper)
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _mapper = mapper;

        }

        public async Task<string> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var endereco = AddressDto.FromDto(request.Address);

            var cliente = new ClientAggregate(            
            request.Name,
            request.Cpf,
            request.Cnpj,
            request.BirthDate,
            request.Phone,
            request.Email,
            endereco,
            request.FreeIE
                   );

            foreach (var @event in cliente.GetUncommittedEvents())
            {
                await _eventStore.SaveAsync(@event);
                await _mediator.Publish(@event);
            }

            return await _idSource.Task;
        }

        public static void SetClienteId(string id)
        {
            _idSource.TrySetResult(id);
        }
    }
}
