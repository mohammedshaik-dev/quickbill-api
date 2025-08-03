using QuickBill.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure
{
    public interface IInvoicePdfRepository
    {
        Task<InvoicePdfDto?> GetInvoicePdfDataAsync(Guid invoiceId);
    }
}
