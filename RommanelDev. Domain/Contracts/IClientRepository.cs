using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Contracts
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(string id);
        Task<Client?> GetByCpfCnpjAsync(string document);
        Task<Client?> GetByEmailAsync(string email);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task RemoveAsync(string id);
    }
}
