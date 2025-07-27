using QuickBill.Domain.DTOs;


namespace QuickBill.Infrastructure
{
    public interface IInvoiceItemRepository
    {
        Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(Guid invoiceId);
        Task<InvoiceItemDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(InvoiceItemDto dto);
        Task<bool> UpdateAsync(InvoiceItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }

}
