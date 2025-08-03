using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuickBill.Application.Document;
using QuickBill.Infrastructure;


namespace QuickBill.Application
{
    public class InvoicePdfService : IInvoicePdfService
    {
        private readonly IInvoicePdfRepository _repository;
        private readonly ILogger<InvoicePdfService> _logger;

        public InvoicePdfService(IInvoicePdfRepository repository, ILogger<InvoicePdfService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId)
        {
            var invoice = await _repository.GetInvoicePdfDataAsync(invoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Quick_Bill_Logo.png");

            var document = new InvoiceDocument(invoice, invoice.Items, logoPath);

            return document.GeneratePdf();
        }

    }
}
