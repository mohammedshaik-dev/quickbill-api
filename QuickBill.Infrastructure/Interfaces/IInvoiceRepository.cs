using QuickBill.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceDto>> GetAllAsync(Guid userId);
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(InvoiceDto dto);
        Task<bool> UpdateAsync(InvoiceDto dto);
        Task<bool> SoftDeleteAsync(Guid id);
    }
}
