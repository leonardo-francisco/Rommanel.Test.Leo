using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Test.UseCase
{
    public class GetClientDtoPJTest
    {
        public static ClientDto GetDefault()
        {

            return new ClientDto
            {
                Name = "Comercio de Ferro Ltda",
                Cpf = null,
                Cnpj = "35276091000177",
                BirthDate = DateTime.Parse("1980-11-22"),
                Phone = "7123345577",
                Email = "ferro.forte@gmail.com",
                Address = new AddressDto
                {
                    ZipCode = "98763220",
                    Street = "Tonico Tinto",
                    Number = "5998",
                    Neighborhood = "Timbo",
                    City = "Esplanada",
                    State = "BA"
                },
                FreeIE = true
            };
        }
    }
}
