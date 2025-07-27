using QuickBill.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Application
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync(Guid userId);
        Task<ClientDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ClientDto dto);
        Task<bool> UpdateAsync(ClientDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
