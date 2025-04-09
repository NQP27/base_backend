using System.Security.Claims;

namespace StockManagement.Domain.DTOs
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public List<Claim> Claims { get; set; }
    }
} 