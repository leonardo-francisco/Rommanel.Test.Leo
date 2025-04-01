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
            CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Cpf) ? null : new CPF(src.Cpf)))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Cnpj) ? null : new CNPJ(src.Cnpj)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(
                src.Address.ZipCode, src.Address.Street, src.Address.Number, src.Address.Neighborhood,
                src.Address.City, src.Address.State)));

            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf != null ? src.Cpf.Value : null))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.Value : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<Client, ClientCreatedEvent>()           
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf != null ? src.Cpf.ToString() : null))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj != null ? src.Cnpj.ToString() : null))           
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(
                src.Address.ZipCode, src.Address.Street, src.Address.Number, src.Address.Neighborhood,
                src.Address.City, src.Address.State)))
            .ForMember(dest => dest.FreeIE, opt => opt.MapFrom(src => src.FreeIE)).ReverseMap();

            CreateMap<Client, ClientUpdatedEvent>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))           
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))          
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(
                src.Address.ZipCode, src.Address.Street, src.Address.Number, src.Address.Neighborhood,
                src.Address.City, src.Address.State)))
            .ForMember(dest => dest.FreeIE, opt => opt.MapFrom(src => src.FreeIE)).ReverseMap();
        }
    }
}
