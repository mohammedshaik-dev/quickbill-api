using QuickBill.Domain.DTOs;

namespace QuickBill.Application
{
    public interface IPaymentDetailsService
    {
        Task<IEnumerable<PaymentDetailsDto>> GetAllByUserIdAsync(Guid userId);
        Task<PaymentDetailsDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(PaymentDetailsDto dto);
        Task<bool> UpdateAsync(PaymentDetailsDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
} 