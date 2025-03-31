using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClienteRemovidoEvent : INotification
    {
        public string ClienteId { get; }

        public ClienteRemovidoEvent(string clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
