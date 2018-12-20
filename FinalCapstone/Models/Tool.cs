using System;

namespace FinalCapstone.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
        public bool CheckedOut { get; set; }
        public string CurrentBorrowerName { get; set; }
        public int CurrentBorrowerId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }
        public string ImageName { get; set; }
    }
}
