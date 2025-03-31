using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class UpdateClienteCommand
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoDto Endereco { get; set; }
        public bool IsentoIE { get; set; }

        public UpdateClienteCommand(string id, string nome, string? cpf, string? cnpj, DateTime dataNascimento, string telefone, string email, EnderecoDto endereco, bool isentoIE)
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
        }
    }
}
