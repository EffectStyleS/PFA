using BLL.DTOs;
using BLL.Infrastructure.Extensions;
using BLL.Interfaces.Identity;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Services.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateToken(UserDTO userDto, UserManager<AppUser> userManager, List<IdentityRole<int>> roles)
        {
            var user = await userManager.FindByNameAsync(userDto.Login);

            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(_configuration);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
