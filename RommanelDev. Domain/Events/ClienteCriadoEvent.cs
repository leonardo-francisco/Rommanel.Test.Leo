using MediatR;
using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClienteCriadoEvent : INotification
    {
        public string ClienteId { get; }
        public string Nome { get; }
        public string? Cpf { get; }
        public string? Cnpj { get; }
        public DateTime DataNascimento { get; }
        public string Telefone { get; }
        public string Email { get; }
        public Endereco Endereco { get; }
        public bool IsentoIE { get; }

        public ClienteCriadoEvent(string clienteId, string nome, string? cpf, string? cnpj, DateTime dataNascimento, string telefone, string email, Endereco endereco, bool isentoIE)
        {
            ClienteId = clienteId;
            Nome = nome;
            Cpf = cpf;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            IsentoIE = isentoIE;
        }
    }
}
