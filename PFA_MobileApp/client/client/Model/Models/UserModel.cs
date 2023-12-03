namespace client.Model.Models
{
    public class UserModel : BaseModel
    {
        public string Login { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}
