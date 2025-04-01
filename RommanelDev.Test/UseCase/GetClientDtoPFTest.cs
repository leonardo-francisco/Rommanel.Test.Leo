using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Test.UseCase
{
    public class GetClientDtoPFTest
    {
        public static ClientDto GetDefault()
        {

            return new ClientDto
            {
                Id = "1",
                Name = "Laila Lopes",
                Cpf = "32702498027",
                Cnpj = null,
                BirthDate = DateTime.Parse("1980-11-22"),
                Phone = "11933665544",
                Email = "laila.lopes@gmail.com",
                Address = new AddressDto
                {
                    ZipCode = "47855369",
                    Street = "Alameda Ucrania",
                    Number = "998",
                    Neighborhood = "Alphaville",
                    City = "Osasco",
                    State = "SP"
                },
                FreeIE = false
            };
        }
    }
}
