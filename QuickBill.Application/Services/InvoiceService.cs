using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure;


namespace QuickBill.Application
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceDto>> GetAllAsync(Guid userId) =>
            await _repository.GetAllAsync(userId);

        public async Task<InvoiceDto?> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Guid> CreateAsync(InvoiceDto dto) =>
            await _repository.CreateAsync(dto);

        public async Task<bool> UpdateAsync(InvoiceDto dto) =>
            await _repository.UpdateAsync(dto);

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repository.SoftDeleteAsync(id);
    }
}
