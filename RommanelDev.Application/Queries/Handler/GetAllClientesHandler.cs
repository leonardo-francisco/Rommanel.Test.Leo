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
    public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<ClienteDto>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public GetAllClientesHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }
    }
}
