using AutoMapper;
using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Queries.Handler
{
    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public GetClienteByIdHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ClienteDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            return cliente is not null ? _mapper.Map<ClienteDto>(cliente) : null;
        }
    }
}
