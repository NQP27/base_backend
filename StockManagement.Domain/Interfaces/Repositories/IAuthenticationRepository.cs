using StockManagement.Domain.DTOs;

namespace StockManagement.Domain.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterDTO registerDto);
        Task<AuthenticationResponse> LoginAsync(LoginDTO loginDto);
        Task<string> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto);
    }
}
