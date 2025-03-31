using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class RemoveClienteCommand : IRequest<bool>
    {
        public string Id { get; }

        public RemoveClienteCommand(string id)
        {
            Id = id;
        }
    }
}
