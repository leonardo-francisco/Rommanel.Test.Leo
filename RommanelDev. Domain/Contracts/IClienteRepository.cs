using RommanelDev._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.Contracts
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(string id);
        Task<Cliente?> GetByCpfCnpjAsync(string documento);
        Task<Cliente?> GetByEmailAsync(string email);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task RemoveAsync(string id);
    }
}
