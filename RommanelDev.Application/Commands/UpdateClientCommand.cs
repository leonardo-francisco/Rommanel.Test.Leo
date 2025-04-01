using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }     
        public AddressDto Address { get; set; }
        public bool FreeIE { get; set; }

        public UpdateClientCommand(string id, string name, DateTime birthDate, string phone, AddressDto address, bool freeIE)
        {
            Id = id;
            Name = name;           
            BirthDate = birthDate;
            Phone = phone;
            Address = address;
            FreeIE = freeIE;
        }
    }
}
