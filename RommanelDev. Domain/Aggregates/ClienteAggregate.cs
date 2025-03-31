using MediatR;
using RommanelDev._Domain.Entities;
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
        public IReadOnlyCollection<INotification> GetUncommittedEvents() => _events.AsReadOnly();

        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Cnpj { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public bool IsentoIE { get; private set; }

        public ClienteAggregate(string id, string nome, string? cpf, string? cnpj, DateTime dataNascimento, string telefone, string email, Endereco endereco, bool isentoIE)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            IsentoIE = isentoIE;

            _events.Add(new ClienteCriadoEvent(id, nome, cpf, cnpj, dataNascimento, telefone, email, endereco, isentoIE));
        }

        private ClienteAggregate() { }

        public static ClienteAggregate FromCliente(Cliente cliente)
        {
            return new ClienteAggregate
            {
                Id = cliente.Id.ToString(),
                Nome = cliente.Nome,
                Cpf = cliente.Cpf?.Value,
                Cnpj = cliente.Cnpj?.Value,
                DataNascimento = cliente.DataNascimento,
                Telefone = cliente.Telefone,
                Email = cliente.Email.Value,
                Endereco = cliente.Endereco,
                IsentoIE = cliente.IsentoIE
            };
        }

        public void Atualizar(string id, string nome, DateTime dataNascimento, string telefone, Endereco endereco, bool isentoIE)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Telefone = telefone;           
            Endereco = endereco;
            IsentoIE = isentoIE;

            _events.Add(new ClienteAtualizadoEvent(id, nome, dataNascimento, telefone, endereco,  isentoIE));
        }

        public void Remover()
        {
            _events.Add(new ClienteRemovidoEvent(Id));
        }

        public void ClearUncommittedEvents()
        {
            _events.Clear();
        }

    }
}
