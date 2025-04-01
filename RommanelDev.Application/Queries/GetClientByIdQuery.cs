using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Queries
{
    public class GetClientByIdQuery : IRequest<ClientDto>
    {
        public string Id { get; }

        public GetClientByIdQuery(string id)
        {
            Id = id;
        }
    }
}
