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
        public string Telefone { get; }
        public string Email { get; }
        public bool IsentoIE { get; }

        public ClienteAtualizadoEvent(string clienteId, string nome, string telefone, string email, bool isentoIE)
        {
            ClienteId = clienteId;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            IsentoIE = isentoIE;
        }
    }
}
