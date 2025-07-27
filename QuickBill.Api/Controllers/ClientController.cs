using Microsoft.AspNetCore.Mvc;
using QuickBill.Application;
using QuickBill.Domain.DTOs;
using QuickBill.Domain.Models.Common;

namespace QuickBill.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        #region Get All Clients
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll([FromQuery] Guid userId)
        {
            var clients = await _service.GetAllAsync(userId);
            return Ok(ApiResponse<IEnumerable<ClientDto>>.Success(clients, "Clients retrieved successfully"));
        }
        #endregion

        #region Get Client by ID

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _service.GetByIdAsync(id);
            if (client == null)
                return NotFound(ApiResponse<string>.Fail("Client not found", 404));

            return Ok(ApiResponse<ClientDto>.Success(client, "Client retrieved successfully"));
        }
        #endregion

        #region Create Client 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<Guid>.Success(id, "Client created successfully"));
        }

        #endregion

        #region Update Client

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientDto dto)
        {
            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound(ApiResponse<string>.Fail("Client not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Client updated successfully"));
        }
        #endregion

        #region
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("Client not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "Client deleted successfully"));
        }
        #endregion
    }
}
