using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBill.Application;
using QuickBill.Domain.Models.Common;

namespace QuickBill.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/invoices")]
    public class InvoicePdfController : ControllerBase
    {
        private readonly IInvoicePdfService _pdfService;

        public InvoicePdfController(IInvoicePdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpGet("{invoiceId}/pdf")]
        public async Task<IActionResult> DownloadInvoicePdf(Guid invoiceId)
        {
            var pdfBytes = await _pdfService.GenerateInvoicePdfAsync(invoiceId);

            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                var errorResponse = new ApiResponse<string>
                {
                    Status = "error",
                    Code = 404,
                    Message = "Invoice not found or PDF generation failed",
                    Data = null,
                    Timestamp = DateTime.UtcNow
                };

                return NotFound(errorResponse);
            }

            var stream = new MemoryStream(pdfBytes);

            return File(
                fileStream: stream,
                contentType: "application/pdf",
                fileDownloadName: $"Invoice_{invoiceId}.pdf"
            );
        }
    }
}
