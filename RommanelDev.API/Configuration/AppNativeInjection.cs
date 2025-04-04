﻿using FluentValidation;
using MediatR;
using RommanelDev._Domain.Contracts;
using RommanelDev.Application.Commands;
using RommanelDev.Application.Commands.Handler;
using RommanelDev.Application.DTO;
using RommanelDev.Application.Map;
using RommanelDev.Application.Queries;
using RommanelDev.Application.Queries.Handler;
using RommanelDev.Application.Validator;
using RommanelDev.Infrastructure.Context;
using RommanelDev.Infrastructure.EventStore;
using RommanelDev.Infrastructure.Repository;

namespace RommanelDev.API.Configuration
{
    public static class AppNativeInjection
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region DbContext
            services.AddSingleton<MongoDbContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
                var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

                return new MongoDbContext(connectionString, databaseName);
            });
            #endregion

            #region Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            #endregion

            #region CQRS
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>, GetAllClientsHandler>();
            services.AddScoped<IRequestHandler<GetClientByIdQuery, ClientDto>, GetClientByIdHandler>();
            services.AddScoped<IRequestHandler<CreateClientCommand, string>, CreateClienteHandler>();
            services.AddScoped<IRequestHandler<UpdateClientCommand, bool>, UpdateClienteHandler>();
            services.AddScoped<IRequestHandler<RemoveClientCommand, bool>, RemoveClienteHandler>();
            #endregion

            #region Events
            services.AddScoped<IEventStore, EventStore>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(MapConfig));
            #endregion

            #region Validator
            services.AddScoped<IValidator<ClientDto>, ClientValidator>();
            services.AddScoped<IValidator<AddressDto>, AddressValidator>();
            #endregion
        }
    }
}
