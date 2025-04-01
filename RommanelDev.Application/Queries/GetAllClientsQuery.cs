using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Queries
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
    }
}
