using MediatR;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Aggregates
{
    public class ClientAggregate
    {
        private readonly List<INotification> _events = new();
        public IReadOnlyCollection<INotification> GetUncommittedEvents() => _events.AsReadOnly();

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string? Cpf { get; private set; }
        public string? Cnpj { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public bool FreeIE { get; private set; }

        public ClientAggregate(string name, string? cpf, string? cnpj, DateTime birthDate, string phone, string email, Address address, bool freeIE)
        {           
            Name = name;
            Cpf = cpf;
            Cnpj = cnpj;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            FreeIE = freeIE;

            _events.Add(new ClientCreatedEvent(Name, Cpf, Cnpj, BirthDate, Phone, Email, Address, FreeIE));
        }

        private ClientAggregate() { }

        public static ClientAggregate FromClient(Client client)
        {
            return new ClientAggregate
            {
                Id = client.Id.ToString(),
                Name = client.Name,
                Cpf = client.Cpf?.Value,
                Cnpj = client.Cnpj?.Value,
                BirthDate = client.BirthDate,
                Phone = client.Phone,
                Email = client.Email.Value,
                Address = client.Address,
                FreeIE = client.FreeIE
            };
        }

        public void Update(string id, string name, DateTime birthDate, string phone, Address address, bool freeIE)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Phone = phone;           
            Address = address;
            FreeIE = freeIE;

            _events.Add(new ClientUpdatedEvent(id, Name, BirthDate, Phone, Address,  FreeIE));
        }

        public void Remover()
        {
            _events.Add(new ClientRemovedEvent(Id));
        }

        public void ClearUncommittedEvents()
        {
            _events.Clear();
        }

    }
}
