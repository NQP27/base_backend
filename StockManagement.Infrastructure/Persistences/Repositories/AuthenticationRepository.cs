using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Domain.Interfaces.Services;

namespace StockManagement.Infrastructure.Persistences.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthenticationRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResponse> RegisterAsync(RegisterDTO registerDto)
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            // Add to default role
            await _userManager.AddToRoleAsync(user, "User");

            return await _jwtService.CreateJwtToken(user);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            if (!user.IsActive)
            {
                throw new Exception("Account is deactivated");
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    throw new Exception("Account is locked out");
                }
                if (result.IsNotAllowed)
                {
                    throw new Exception("Account is not allowed to login");
                }
                throw new Exception("Invalid email or password");
            }

            // Update last login time
            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return await _jwtService.CreateJwtToken(user);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return null;
            }

            if (!user.IsActive)
            {
                throw new Exception("Account is deactivated");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return false;
            }

            if (!user.IsActive)
            {
                throw new Exception("Account is deactivated");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to reset password: {errors}");
            }

            return true;
        }
    }
}
