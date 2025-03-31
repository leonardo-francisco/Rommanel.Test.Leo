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
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<Cliente> _collection;

        public ClienteRepository(MongoDbContext context)
        {
            _collection = context.Clientes;
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _collection.InsertOneAsync(cliente);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Cliente?> GetByCpfCnpjAsync(string documento)
        {
            var customer = await _collection.Find(c => c.Cpf.Value == documento || c.Cnpj.Value == documento)
                                    .FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            var customer = await _collection.Find(x => x.Email.Value == email).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Cliente?> GetByIdAsync(string id)
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

        public async Task UpdateAsync(Cliente cliente)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Id, cliente.Id);
            var update = Builders<Cliente>.Update
                .Set(c => c.Nome, cliente.Nome)
                .Set(c => c.Cpf, cliente.Cpf)
                .Set(c => c.Cnpj, cliente.Cnpj)
                .Set(c => c.DataNascimento, cliente.DataNascimento)
                .Set(c => c.Telefone, cliente.Telefone)
                .Set(c => c.Email, cliente.Email)
                .Set(c => c.Endereco, cliente.Endereco)
                .Set(c => c.IsentoIE, cliente.IsentoIE);

            var result = await _collection.UpdateOneAsync(filter, update);
        }
    }
}
