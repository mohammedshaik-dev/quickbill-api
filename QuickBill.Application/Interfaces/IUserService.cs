using QuickBill.Domain.DTOs;


namespace QuickBill.Application
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(UserDto user);
        Task<bool> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(Guid id);
    }
}
