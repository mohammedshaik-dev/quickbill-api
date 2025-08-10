using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure;

namespace QuickBill.Application
{
    public class PaymentDetailsService : IPaymentDetailsService
    {
        private readonly IPaymentDetailsRepository _repository;

        public PaymentDetailsService(IPaymentDetailsRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PaymentDetailsDto>> GetAllByUserIdAsync(Guid userId) => _repository.GetAllByUserIdAsync(userId);
        public Task<PaymentDetailsDto?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task<Guid> CreateAsync(PaymentDetailsDto dto) => _repository.CreateAsync(dto);
        public Task<bool> UpdateAsync(PaymentDetailsDto dto) => _repository.UpdateAsync(dto);
        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
} 