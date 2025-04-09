using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Repositories;
using System.Security.Claims;

namespace StockManagement.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationRepository _authenRepo;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IAuthenticationRepository authenRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authenRepo = authenRepo;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var result = await _authenRepo.RegisterAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering user", error = ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var result = await _authenRepo.LoginAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while logging in", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var token = await _authenRepo.ForgotPasswordAsync(model);
                if (token == null)
                {
                    // Don't reveal that the user does not exist
                    return Ok(new { message = "If your email is registered, you will receive a password reset link." });
                }

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing forgot password request", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var result = await _authenRepo.ResetPasswordAsync(model);
                if (!result)
                {
                    return BadRequest(new { message = "Invalid request" });
                }

                return Ok(new { message = "Password has been reset successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while resetting password", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("external-login")]
        public IActionResult ExternalLogin(string provider = GoogleDefaults.AuthenticationScheme, string? returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Identity", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                return BadRequest(new { message = $"Error from external provider: {remoteError}" });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest(new { message = "Error loading external login information." });
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    return StatusCode(500, new { message = "Cannot find user after successful external sign in." });
                }
                var tokenResult = await _authenRepo.GenerateJwtToken(user);
                if (tokenResult == null)
                {
                    return StatusCode(500, new { message = "Failed to generate token after external login." });
                }
                return Ok(tokenResult);
            }

            if (signInResult.IsLockedOut)
            {
                return StatusCode(423, new { message = "User account locked out." });
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;
                var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;

                if (email == null)
                {
                    return BadRequest(new { message = "Email claim not received from external provider. Cannot proceed." });
                }

                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser != null)
                {
                    var addLoginResult = await _userManager.AddLoginAsync(existingUser, info);
                    if (!addLoginResult.Succeeded)
                    {
                        return StatusCode(500, new { message = "Error linking external account to existing user.", errors = addLoginResult.Errors });
                    }
                    await _signInManager.SignInAsync(existingUser, isPersistent: false);
                    var tokenResult = await _authenRepo.GenerateJwtToken(existingUser);
                    if (tokenResult == null)
                    {
                        return StatusCode(500, new { message = "Failed to generate token after linking account.", errors = tokenResult });
                    }
                    return Ok(tokenResult);
                }
                else
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        EmailConfirmed = true
                    };

                    var createUserResult = await _userManager.CreateAsync(newUser);
                    if (createUserResult.Succeeded)
                    {
                        var addLoginResult = await _userManager.AddLoginAsync(newUser, info);
                        if (addLoginResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(newUser, isPersistent: false);
                            var tokenResult = await _authenRepo.GenerateJwtToken(newUser);
                            if (tokenResult == null)
                            {
                                return StatusCode(500, new { message = "Failed to generate token for new user.", errors = tokenResult });
                            }
                            return Ok(tokenResult);
                        }
                        await _userManager.DeleteAsync(newUser);
                        return StatusCode(500, new { message = "Error linking external account to newly created user.", errors = addLoginResult.Errors });
                    }
                    return StatusCode(500, new { message = "Error creating new user.", errors = createUserResult.Errors });
                }
            }
        }

    }
}