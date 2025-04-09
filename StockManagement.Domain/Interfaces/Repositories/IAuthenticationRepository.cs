using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace StockManagement.Domain.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterDTO registerDto);
        Task<AuthenticationResponse> LoginAsync(LoginDTO loginDto);
        Task<string> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto);
        Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user);
    }
}
