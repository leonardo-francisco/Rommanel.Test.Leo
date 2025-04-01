using MediatR;
using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClientUpdatedEvent : INotification
    {
        public string ClientId { get; }
        public string Name { get; }
        public DateTime BirthDate { get; }
        public string Phone { get; }
        public Address Address { get; }
        public bool FreeIE { get; }

        public ClientUpdatedEvent(string clientId, string name, DateTime birthDate, string phone, Address address, bool freeIE)
        {
            ClientId = clientId;
            Name = name;
            BirthDate = birthDate;
            Phone = phone;
            Address = address;
            FreeIE = freeIE;
        }
    }
}
