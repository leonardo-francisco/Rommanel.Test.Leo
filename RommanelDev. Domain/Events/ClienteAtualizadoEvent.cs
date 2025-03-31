using MediatR;
using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Events
{
    public class ClienteAtualizadoEvent : INotification
    {
        public string ClienteId { get; }
        public string Nome { get; }
        public DateTime DataNascimento { get; }
        public string Telefone { get; }
        public Endereco Endereco { get; }
        public bool IsentoIE { get; }

        public ClienteAtualizadoEvent(string clienteId, string nome, DateTime dataNascimento, string telefone, Endereco endereco, bool isentoIE)
        {
            ClienteId = clienteId;
            Nome = nome;
            DataNascimento = dataNascimento;
            Telefone = telefone;           
            Endereco = endereco;
            IsentoIE = isentoIE;
        }
    }
}
