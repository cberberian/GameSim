using SysComponent = System.ComponentModel.DataAnnotations;

namespace SimGame.Website.Models
{
    public class Register
    {
        [SysComponent.RequiredAttribute(ErrorMessage = "Username is required.")]

        public string UserName { get; set; }

        [SysComponent.RequiredAttribute(ErrorMessage = "Username is required.")]
        public string Password { get; set; }
        [SysComponent.CompareAttribute("Password", ErrorMessage = "Passwords Don't Match")]
        public string ConfirmPassword { get; set; }
    }
}