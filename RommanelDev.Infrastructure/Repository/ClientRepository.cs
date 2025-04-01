using MongoDB.Bson;
using MongoDB.Driver;
using RommanelDev._Domain.Contracts;
using RommanelDev._Domain.Entities;
using RommanelDev._Domain.ValueObjects;
using RommanelDev.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RommanelDev.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;

        public ClientRepository(MongoDbContext context)
        {
            _collection = context.Clients;
        }

        public async Task<Client> AddAsync(Client client)
        {
            await _collection.InsertOneAsync(client);
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Client?> GetByCpfCnpjAsync(string document)
        {
            var customer = await _collection.Find(c => c.Cpf.Value == document || c.Cnpj.Value == document)
                                    .FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            var customer = await _collection.Find(x => x.Email.Value == email).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Client?> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var customer = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return customer;
        }

        public async Task RemoveAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task UpdateAsync(Client client)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, client.Id);
            var update = Builders<Client>.Update
                .Set(c => c.Name, client.Name)              
                .Set(c => c.BirthDate, client.BirthDate)
                .Set(c => c.Phone, client.Phone)                
                .Set(c => c.Address, client.Address)
                .Set(c => c.FreeIE, client.FreeIE);

            var result = await _collection.UpdateOneAsync(filter, update);
        }
    }
}
