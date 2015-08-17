using System.ComponentModel.DataAnnotations;

namespace MvcApplication2.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Username is required.")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords Don't Match")]
        public string ConfirmPassword { get; set; }
    }
}