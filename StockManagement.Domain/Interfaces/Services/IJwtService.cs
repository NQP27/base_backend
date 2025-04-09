using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockManagement.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);
        Task<ClaimsPrincipal> GetPrincipalFromToken(string token);
        Task<JwtSecurityToken> GenerateResetPasswordJwtToken(ApplicationUser user);
    }
}