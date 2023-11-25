namespace API.RequestsModels.Identity
{
    public class AuthResponse
    {
        public string Login { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
