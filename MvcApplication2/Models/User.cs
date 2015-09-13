using System.ComponentModel.DataAnnotations;

namespace SimGame.Website.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}