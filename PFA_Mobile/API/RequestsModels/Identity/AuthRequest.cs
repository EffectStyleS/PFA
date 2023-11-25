namespace API.RequestsModels.Identity
{
    public class AuthRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
