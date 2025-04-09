using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Repositories;

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

        //private async JwtSecurityToken GenerateJwtToken(ApplicationUser user, IList<string> roles)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        //    };

        //    // Add roles to claims
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

        //    var token = new JwtSecurityToken(
        //        _configuration["Jwt:Issuer"],
        //        _configuration["Jwt:Issuer"],
        //        claims,
        //        expires: expires,
        //        signingCredentials: creds
        //    );

        //    return token;
        //}
    }
}