using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiClient;

/// <summary>
/// Клиент
/// </summary>
public partial class Client
{
    /// <summary>
    /// Добавляет в заголовок авторизации JWT
    /// </summary>
    /// <param name="accessToken">JWT</param>
    public void AddBearerToken(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

    /// <summary>
    /// Сбрасывает JWT из заголовка
    /// </summary>
    public void ResetBearerToken() => _httpClient.DefaultRequestHeaders.Authorization = null;

    /// <summary>
    /// Получает логин пользователя из токена
    /// </summary>
    /// <returns></returns>
    public string GetCurrentUserLogin()
    {
        var stream = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(stream);
        var tokenS = jsonToken as JwtSecurityToken;

        return tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;          
    }
}