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
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IPaymentDetailsService _service;

        public PaymentDetailsController(IPaymentDetailsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid userId)
        {
            var items = await _service.GetAllByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<PaymentDetailsDto>>.Success(items, "Payment details retrieved successfully"));
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound(ApiResponse<string>.Fail("Payment details not found", 404));

            return Ok(ApiResponse<PaymentDetailsDto>.Success(item, "Payment details retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentDetailsDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<Guid>.Success(id, "Payment details created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PaymentDetailsDto dto)
        {
            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound(ApiResponse<string>.Fail("Payment details not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Payment details updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("Payment details not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Payment details deleted successfully"));
        }
    }
} 