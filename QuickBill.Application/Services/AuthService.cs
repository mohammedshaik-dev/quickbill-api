using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QuickBill.Domain;
using QuickBill.Domain.Models.Common;
using QuickBill.Infrastructure;
using QuickBill.Domain.DTOs;
using Microsoft.Extensions.Options;

namespace QuickBill.Application.Services
{
    public class AuthService : IAuthService {

        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || user.IsDeleted || !user.IsActive)
                return null;

            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
            var token = GenerateJwtToken(user, expiresAt);
            return new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role,
                Token = token,
                ExpiresAt = expiresAt
            };
        }

        private bool VerifyPassword(string plain, string hash)
        {
            // 🔐 For demo: plain text check. Replace with hash validation.
            return plain == hash;
        }

        private string GenerateJwtToken(UserDto user, DateTime expiresAt)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("name", user.Name),
        };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
