using DAL.Entities;

namespace BLL.DTOs
{
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(AppUser user)
        {
            Id       = user.Id;
            Login    = user.Login;

            RefreshToken           = user.RefreshToken;
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;
        }

        public int Id { get; set; }
        public string Login { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }


}
