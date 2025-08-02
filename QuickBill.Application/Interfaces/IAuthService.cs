using QuickBill.Domain;

namespace QuickBill.Application
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto);
    }
}
