using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Email:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
