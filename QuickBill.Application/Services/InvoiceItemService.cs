using QuickBill.Domain.DTOs;
using QuickBill.Infrastructure;

namespace QuickBill.Application
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IInvoiceItemRepository _repository;

        public InvoiceItemService(IInvoiceItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(Guid invoiceId) =>
            await _repository.GetAllByInvoiceIdAsync(invoiceId);

        public async Task<InvoiceItemDto?> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Guid> CreateAsync(InvoiceItemDto dto) =>
            await _repository.CreateAsync(dto);

        public async Task<bool> UpdateAsync(InvoiceItemDto dto) =>
            await _repository.UpdateAsync(dto);

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
