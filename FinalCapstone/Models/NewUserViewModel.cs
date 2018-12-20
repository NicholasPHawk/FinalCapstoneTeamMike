using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Drivers License Number:")]
        public string DriversLicense { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Address:")]
        public string Address { get; set; }
    }
}
