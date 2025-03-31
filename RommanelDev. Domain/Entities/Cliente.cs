using RommanelDev._Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public CPF? Cpf { get; set; }
        public CNPJ? Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public Email Email { get; set; }
        public Endereco Endereco { get; set; }
        public bool IsentoIE { get; set; }

        public Cliente() { }

        public Cliente(string nome, CPF? cpf, CNPJ? cnpj, DateTime dataNascimento, string telefone, Email email, Endereco endereco, bool isentoIE)
        {
            Nome = nome;
            Cpf = cpf;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            IsentoIE = isentoIE;

            ValidarCliente();
        }

        private void ValidarCliente()
        {
            if (Cpf is not null && Cnpj is not null)
                throw new ArgumentException("Um cliente não pode ter CPF e CNPJ ao mesmo tempo.");

            if (Cpf is not null && DateTime.Now.Year - DataNascimento.Year < 18)
                throw new ArgumentException("A idade mínima para cadastro é 18 anos.");

            if (Cnpj is not null && !IsentoIE)
                throw new ArgumentException("Pessoa Jurídica deve informar a IE ou ser isenta.");
        }

        public void Atualizar(string nome, DateTime dataNascimento, string telefone, Endereco endereco, bool isentoIE)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Endereco = endereco;
            IsentoIE = isentoIE;
        }
    }
}
