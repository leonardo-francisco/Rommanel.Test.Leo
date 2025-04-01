using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class CreateClientCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public bool FreeIE { get; set; }

        public CreateClientCommand(string name, string? cpf, string? cnpj, DateTime birthDate, string phone, string email, AddressDto address, bool freeIE)
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
