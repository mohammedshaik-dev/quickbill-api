using QuickBill.Domain;
using QuickBill.Domain.GoogleDTOs;

namespace QuickBill.Application
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto);

        Task<GoogleLoginResponseDto?> LoginWithGoogleAsync(string idToken);
    }
}
