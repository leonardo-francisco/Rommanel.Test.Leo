using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClientRemovedEvent : INotification
    {
        public string ClientId { get; }

        public ClientRemovedEvent(string clientId)
        {
            ClientId = clientId;
        }
    }
}
