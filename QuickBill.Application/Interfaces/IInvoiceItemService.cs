using QuickBill.Domain.DTOs;


namespace QuickBill.Application
{
    public interface IInvoiceItemService
    {
        Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(Guid invoiceId);
        Task<InvoiceItemDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(InvoiceItemDto dto);
        Task<bool> UpdateAsync(InvoiceItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
