using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiClient
{
    public partial class Client
    {
        public void AddBearerToken(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        public void ResetBearerToken() => _httpClient.DefaultRequestHeaders.Authorization = null;

        public string GetCurrentUserLogin()
        {
            var stream = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            return tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;          
        }
    }
}
