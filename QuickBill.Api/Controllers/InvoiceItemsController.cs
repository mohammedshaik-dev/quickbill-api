using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBill.Application;
using QuickBill.Domain.DTOs;
using QuickBill.Domain.Models.Common;

namespace QuickBill.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IInvoiceItemService _invoiceItemService;

        public InvoiceItemsController(IInvoiceItemService service)
        {
            _invoiceItemService = service;
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(Guid invoiceId)
        {
            var result = await _invoiceItemService.GetAllByInvoiceIdAsync(invoiceId);
            return Ok(ApiResponse<IEnumerable<InvoiceItemDto>>.Success(result, "Invoice items loaded successfully"));
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _invoiceItemService.GetByIdAsync(id);
            if (result == null)
                return NotFound(ApiResponse<string>.Fail("Invoice item not found", 404));

            return Ok(ApiResponse<InvoiceItemDto>.Success(result, "Invoice item retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceItemDto dto)
        {
            var id = await _invoiceItemService.CreateAsync(dto);
            return Ok(ApiResponse<Guid>.Success( id, "Invoice item created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InvoiceItemDto dto)
        {
            var updated = await _invoiceItemService.UpdateAsync(dto);
            if (!updated)
                return NotFound(ApiResponse<string>.Fail("Invoice item not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Invoice item updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _invoiceItemService.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("Invoice item not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Invoice item deleted successfully"));
        }
    }
}
