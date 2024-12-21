using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
