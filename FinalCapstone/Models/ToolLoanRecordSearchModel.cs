using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class ToolLoanRecordSearchModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
        public bool CheckedOut { get; set; }
        public string LicenseNumber { get; set; }
        public string CurrentBorrowerName { get; set; }
        public int CurrentBorrowerId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }
        public string ImageName
        {
            get
            {
                return Brand + ToolName;
            }
            set
            {
                ImageName = $"{Brand} {ToolName}";
            }
        }
        public string SearchString { get; set; }

        public string SearchType { get; set; }

        public static List<SelectListItem> SearchOptions = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Name", Value = "Name" },
            new SelectListItem() { Text = "License", Value = "License" },
            new SelectListItem() { Text = "Tool Number", Value = "Tool Number" },
        };
    }
}
