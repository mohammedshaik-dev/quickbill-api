using QuickBill.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(UserDto user);
        Task<bool> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(Guid id);

        Task<UserDto?> GetByEmailAsync(string email);
    }
}
