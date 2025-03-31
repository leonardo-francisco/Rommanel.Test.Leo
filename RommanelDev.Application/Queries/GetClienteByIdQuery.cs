using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Queries
{
    public class GetClienteByIdQuery : IRequest<ClienteDto>
    {
        public string Id { get; }

        public GetClienteByIdQuery(string id)
        {
            Id = id;
        }
    }
}
