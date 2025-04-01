using RommanelDev._Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Entities
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public CPF? Cpf { get; set; }
        public CNPJ? Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }
        public bool FreeIE { get; set; }

        public Client() { }

        public Client(string name, CPF? cpf, CNPJ? cnpj, DateTime birthDate, string phone, Email email, Address address, bool freeIE)
        {
            Name = name;
            Cpf = cpf;
            Cnpj = cnpj;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            FreeIE = freeIE;

            ValidarCliente();
        }

        private void ValidarCliente()
        {
            if (Cpf is not null && Cnpj is not null)
                throw new ArgumentException("Um cliente não pode ter CPF e CNPJ ao mesmo tempo.");

            if (Cpf is not null && DateTime.Now.Year - BirthDate.Year < 18)
                throw new ArgumentException("A idade mínima para cadastro é 18 anos.");

            if (Cnpj is not null && !FreeIE)
                throw new ArgumentException("Pessoa Jurídica deve informar a IE ou ser isenta.");
        }

        public void Atualizar(string name, DateTime birthDate, string phone, Address address, bool freeIE)
        {
            Name = name;
            BirthDate = birthDate;
            Phone = phone;
            Address = address;
            FreeIE = freeIE;
        }
    }
}
