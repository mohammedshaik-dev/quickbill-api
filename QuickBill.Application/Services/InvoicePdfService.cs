using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuickBill.Application.Document;
using QuickBill.Infrastructure;
using Microsoft.Extensions.Configuration;


namespace QuickBill.Application
{
    public class InvoicePdfService : IInvoicePdfService
    {
        private readonly IInvoicePdfRepository _repository;
        private readonly ILogger<InvoicePdfService> _logger;
        private readonly IConfiguration _configuration;

        public InvoicePdfService(IInvoicePdfRepository repository, ILogger<InvoicePdfService> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId)
        {
            var invoice = await _repository.GetInvoicePdfDataAsync(invoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Quick_Bill_Logo.png");

            var companyName = _configuration["Company:Name"] ?? string.Empty;
            var companyAddress = _configuration["Company:Address"] ?? string.Empty;
            var companyEmail = _configuration["Company:Email"] ?? string.Empty;
            var companyPhone = _configuration["Company:Phone"] ?? string.Empty;

            var document = new InvoiceDocument(
                invoice,
                invoice.Items,
                logoPath,
                companyName,
                companyAddress,
                companyEmail,
                companyPhone);

            return document.GeneratePdf();
        }

    }
}
