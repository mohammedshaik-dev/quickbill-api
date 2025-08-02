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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.Success(users, "User list retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound(ApiResponse<string>.Fail("User not found", 404));

            return Ok(ApiResponse<UserDto>.Success(user, "User retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            var id = await _service.CreateAsync(user);
            return Ok(ApiResponse<Guid>.Success(id, "User created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto user)
        {
            var updated = await _service.UpdateAsync(user);
            if (!updated)
                return NotFound(ApiResponse<string>.Fail("User not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "User updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("User not found", 404));

            return Ok(ApiResponse<bool>.Success(true, "User deleted successfully"));
        }
    }
}
