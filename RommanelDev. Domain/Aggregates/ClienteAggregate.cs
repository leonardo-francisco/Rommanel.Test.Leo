using RommanelDev._Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Aggregates
{
    public class ClienteAggregate
    {
        private readonly List<INotification> _events = new();

        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Cnpj { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public bool IsentoIE { get; private set; }

        public ClienteAggregate(string id, string nome, string? cpf, string? cnpj, DateTime dataNascimento, string telefone, string email, bool isentoIE)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            IsentoIE = isentoIE;

            _events.Add(new ClienteCriadoEvent(id, nome, cpf, cnpj, dataNascimento, telefone, email, isentoIE));
        }

        public void Atualizar(string nome, string telefone, string email, bool isentoIE)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            IsentoIE = isentoIE;

            _events.Add(new ClienteAtualizadoEvent(Id, nome, telefone, email, isentoIE));
        }

        public void Remover()
        {
            _events.Add(new ClienteRemovidoEvent(Id));
        }

        public IReadOnlyCollection<INotification> GetUncommittedEvents() => _events;
    }
}
