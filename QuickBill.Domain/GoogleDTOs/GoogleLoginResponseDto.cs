using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Domain.GoogleDTOs
{
    public class GoogleLoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpiresAt { get; set; }
    }
}
