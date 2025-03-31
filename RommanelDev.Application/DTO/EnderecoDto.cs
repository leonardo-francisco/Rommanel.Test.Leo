using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.DTO
{
    public class EnderecoDto
    {
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }

        public static Endereco FromDto(EnderecoDto enderecoDto)
        {
            return new Endereco(                                                
                enderecoDto.CEP,
                enderecoDto.Logradouro,
                enderecoDto.Numero,
                enderecoDto.Bairro,
                enderecoDto.Cidade,
                enderecoDto.Estado
            );
        }
    }

   
}
