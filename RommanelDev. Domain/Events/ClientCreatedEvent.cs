using MediatR;
using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClientCreatedEvent : INotification
    {       
        public string Name { get; }
        public string? Cpf { get; }
        public string? Cnpj { get; }
        public DateTime BirthDate { get; }
        public string Phone { get; }
        public string Email { get; }
        public Address Address { get; }
        public bool FreeIE { get; }

        public ClientCreatedEvent(string name, string? cpf, string? cnpj, DateTime birthDate, string phone, string email, Address address, bool freeIE)
        {            
            Name = name;
            Cpf = cpf;
            Cnpj = cnpj;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            FreeIE = freeIE;
        }
    }
}
