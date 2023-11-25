namespace client.Model.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpireTime { get; set; } // хз нужно ли
    }
}
