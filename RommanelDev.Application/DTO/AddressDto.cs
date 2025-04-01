using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.DTO
{
    public class AddressDto
    {
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public static Address FromDto(AddressDto addressDto)
        {
            return new Address(                                                
                addressDto.ZipCode,
                addressDto.Street,
                addressDto.Number,
                addressDto.Neighborhood,
                addressDto.City,
                addressDto.State
            );
        }
    }

   
}
