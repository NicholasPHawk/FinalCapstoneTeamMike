using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "This field is required")]        
        [Display(Name = "New Password:")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
    }
}
