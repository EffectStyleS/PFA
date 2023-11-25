using System.ComponentModel.DataAnnotations;

namespace API.RequestsModels.Identity
{
    public class RegisterRequest
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = null!;
    }
}
