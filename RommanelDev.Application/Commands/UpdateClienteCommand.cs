using MediatR;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Commands
{
    public class UpdateClienteCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }     
        public EnderecoDto Endereco { get; set; }
        public bool IsentoIE { get; set; }

        public UpdateClienteCommand(string id, string nome, DateTime dataNascimento, string telefone, EnderecoDto endereco, bool isentoIE)
        {
            Id = id;
            Nome = nome;           
            DataNascimento = dataNascimento;
            Telefone = telefone;            
            Endereco = endereco;
            IsentoIE = isentoIE;
        }
    }
}
