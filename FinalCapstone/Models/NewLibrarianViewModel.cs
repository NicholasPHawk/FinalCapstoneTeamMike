using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public string  ErrorMessage { get; set; }
    }
}