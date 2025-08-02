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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            var result = await _service.GetAllAsync(userId);
            return Ok(ApiResponse<IEnumerable<InvoiceDto>>.Success(result, "Invoices loaded successfully"));
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(ApiResponse<string>.Fail("Invoice not found", 404));

            return Ok(ApiResponse<InvoiceDto>.Success(result, "Invoice details loaded"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<Guid>.Success(id, "Invoice created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InvoiceDto dto)
        {
            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound(ApiResponse<string>.Fail("Invoice not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Invoice updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("Invoice not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Invoice deleted successfully"));
        }
    }
}
