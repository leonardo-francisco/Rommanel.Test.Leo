using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.DTO
{
    public class ClienteDto
    {
        public string? Id { get; set; }
        public string Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoDto Endereco { get;  set; }
        public bool IsentoIE { get; set; }
    }
}
