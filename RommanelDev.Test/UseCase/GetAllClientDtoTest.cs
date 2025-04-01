using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Test.UseCase
{
    public class GetAllClientDtoTest
    {
        public static List<ClientDto> GetDefault()
        {

            return new List<ClientDto>
            {
                new ClientDto
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
                },
                new ClientDto
                {
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
                }
            };
        }
    }
}
