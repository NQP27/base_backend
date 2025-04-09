using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockManagement.Domain.DTOs;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManagement.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await GetUserClaims(user, roles);

            var token = GenerateToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse
            {
                Token = tokenString,
                Expiration = token.ValidTo,
                UserId = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList(),
                Claims = claims.ToList()
            };
        }

        public async Task<ClaimsPrincipal> GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<JwtSecurityToken> GenerateResetPasswordJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Purpose", "ResetPassword")
            };

            return GenerateToken(claims, TimeSpan.FromHours(1)); // Reset password token expires in 1 hour
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            // Add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenExpiration = expiration ?? TimeSpan.FromDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(tokenExpiration),
                signingCredentials: creds
            );

            return token;
        }
    }
}