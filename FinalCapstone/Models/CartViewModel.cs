using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FinalCapstone.Models
{
    public class CartViewModel
    {
        public IList<Tool> Tools { get; set; }
        public IList<User> Users { get; set; }

        public int Id { get; set; }
        public string Borrower { get; set; }
        public int Days { get; set; }

        public List<SelectListItem> Borrowers { get; set; }

        public List<SelectListItem> DayChoice = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "1", Value = "1" },
            new SelectListItem() { Text = "2", Value = "2" },
            new SelectListItem() { Text = "3", Value = "3" },
            new SelectListItem() { Text = "4", Value = "4" },
            new SelectListItem() { Text = "5", Value = "5" },
            new SelectListItem() { Text = "6", Value = "6" },
            new SelectListItem() { Text = "7", Value = "7" }
        };
    }
}
