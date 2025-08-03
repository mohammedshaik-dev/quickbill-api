using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Application
{
    public interface IInvoicePdfService
    {
        Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId);
    }
}
