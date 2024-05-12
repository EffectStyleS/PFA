using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PFA_Mobile_v2.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace PFA_Mobile_v2.Infrastructure.Authentication.Extensions;

/// <summary>
/// Расширения для работы с JWT по схеме Bearer
/// </summary>
public static class JwtBearerExtensions
{
    /// <summary>
    /// Создает claims пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="roles">Коллекция ролей</param>
    /// <returns></returns>
    public static List<Claim> CreateClaims(this AppUser user, IEnumerable<IdentityRole<int>> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Role, string.Join(" ", roles.Select(x => x.Name))),
        };

        return claims;
    }

    /// <summary>
    /// Создает данные подписи
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Данные подписи</returns>
    public static SigningCredentials CreateSigningCredentials(this IConfiguration configuration)
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }

    /// <summary>
    /// Создает JWT
    /// </summary>
    /// <param name="claims">Коллекция claims</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Json Web Token</returns>
    public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var expireFromConfiguration = configuration.GetSection("Jwt:Expire").Value;
        var expire = int.Parse(expireFromConfiguration!);

        return new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(expire),
            signingCredentials: configuration.CreateSigningCredentials()
        );
    }

    /// <summary>
    /// Создает JWT
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    /// <param name="authClaims">Коллекция claims</param>
    /// <returns>Json Web Token</returns>
    public static JwtSecurityToken CreateToken(this IConfiguration configuration, IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
        var tokenValidityInMinutesFromConfiguration = configuration.GetSection("Jwt:TokenValidityInMinutes").Value;
        var tokenValidityInMinutes = int.Parse(tokenValidityInMinutesFromConfiguration!);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    /// <summary>
    /// Генерирует refresh токен
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Base64 представления refresh токена</returns>
    public static string GenerateRefreshToken(this IConfiguration configuration)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Получает контекст безлопасности пользователя из просроченного токена
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    /// <param name="token">JWT</param>
    /// <returns>Контекст безлопасности пользователя</returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(this IConfiguration configuration, string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}