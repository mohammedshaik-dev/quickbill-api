using QuickBill.Domain.DTOs;


namespace QuickBill.Infrastructure
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientDto>> GetAllAsync(Guid userId);
        Task<ClientDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ClientDto dto);
        Task<bool> UpdateAsync(ClientDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
