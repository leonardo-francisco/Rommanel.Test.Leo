using AutoMapper;
using MongoDB.Bson;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.Events;
using RommanelDev._Domain.ValueObjects;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Map
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<ClienteDto, Cliente>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Cpf) ? null : new CPF(src.Cpf)))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Cnpj) ? null : new CNPJ(src.Cnpj)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email)))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco(
                src.Endereco.CEP, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Bairro,
                src.Endereco.Cidade, src.Endereco.Estado)));

            CreateMap<Cliente, ClienteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf != null ? src.Cpf.Value : null))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.Value : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<EnderecoDto, Endereco>();
            CreateMap<Endereco, EnderecoDto>();

            CreateMap<Cliente, ClienteCriadoEvent>()
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf != null ? src.Cpf.ToString() : null))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.ToString() : null))
            .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento))
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToString()))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco(
                src.Endereco.CEP, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Bairro,
                src.Endereco.Cidade, src.Endereco.Estado)))
            .ForMember(dest => dest.IsentoIE, opt => opt.MapFrom(src => src.IsentoIE)).ReverseMap();

            CreateMap<Cliente, ClienteAtualizadoEvent>()
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))           
            .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento))
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))          
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco(
                src.Endereco.CEP, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Bairro,
                src.Endereco.Cidade, src.Endereco.Estado)))
            .ForMember(dest => dest.IsentoIE, opt => opt.MapFrom(src => src.IsentoIE)).ReverseMap();
        }
    }
}
