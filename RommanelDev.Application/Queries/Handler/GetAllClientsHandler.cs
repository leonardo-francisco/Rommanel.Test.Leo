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
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsHandler(IClientRepository clienteRepository, IMapper mapper)
        {
            _clientRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clientes);
        }
    }
}
