using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Domain.Entities;
using PFA_Mobile_v2.Infrastructure.Authentication.Extensions;

namespace PFA_Mobile_v2.Infrastructure.Authentication.Services;

/// <summary>
/// Сервис токенов
/// </summary>
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Сервис токенов
    /// </summary>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <inheritdoc />
    public async Task<string> CreateToken(UserDto userDto, UserManager<AppUser> userManager, List<IdentityRole<int>> roles)
    {
        var user = await userManager.FindByNameAsync(userDto.Login);

        if (user is null)
        {
            return string.Empty;
        }
        
        var token = user
            .CreateClaims(roles)
            .CreateJwtToken(_configuration);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}