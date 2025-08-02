using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickBill.Domain;
using QuickBill.Domain.DTOs;
using QuickBill.Domain.GoogleDTOs;
using QuickBill.Domain.Models.Common;
using QuickBill.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickBill.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
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

        public async Task<GoogleLoginResponseDto?> LoginWithGoogleAsync(string idToken)
        {

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

            var email = payload.Email;
            var name = payload.Name;

            _logger.LogInformation("Google token validated for email: {Email}", email);

            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                _logger.LogInformation("New Google user. Creating user in DB...");

                var newUser = new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Role = "User",
                    IsActive = true,
                    IsDeleted = false,
                    PasswordHash = "GoogleAuth" // placeholder (won’t be used)
                };

                var createdUserId = await _userRepository.CreateAsync(newUser);
                newUser.Id = createdUserId;
                existingUser = newUser;

                _logger.LogInformation("User created with ID: {UserId}", createdUserId);
            }

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
            var token = GenerateJwtToken(existingUser, expiresAt);

            _logger.LogInformation("JWT generated for user: {Email}", email);

            return new GoogleLoginResponseDto
            {
                UserId = existingUser.Id,
                Email = existingUser.Email,
                Role = existingUser.Role,
                Token = token,
                TokenExpiresAt = expiresAt
            };

        }
    }
}
