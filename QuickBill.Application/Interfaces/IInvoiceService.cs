using QuickBill.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Application
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllAsync(Guid userId);
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(InvoiceDto dto);
        Task<bool> UpdateAsync(InvoiceDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
