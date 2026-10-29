using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopera.Application.IService.User;
using Shopera.Domain.Entities;

namespace Shopera.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(
            ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName ?? ""),

                new Claim(
                    ClaimTypes.Email,
                    user.Email ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(
                    _configuration["Jwt:ExpireMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}