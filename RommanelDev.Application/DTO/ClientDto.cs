using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.DTO
{
    public class ClientDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public AddressDto Address { get;  set; }
        public bool FreeIE { get; set; }
    }
}
