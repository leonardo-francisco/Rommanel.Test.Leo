using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class RemoveClientCommand : IRequest<bool>
    {
        public string Id { get; }

        public RemoveClientCommand(string id)
        {
            Id = id;
        }
    }
}
