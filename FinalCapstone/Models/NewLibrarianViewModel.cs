using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class NewLibrarianViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
    }
}