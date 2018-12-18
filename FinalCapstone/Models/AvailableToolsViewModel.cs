using System.Collections.Generic;

namespace FinalCapstone.Models
{
    public class AvailableToolsViewModel
    {
        public IList<Tool> Tools { get; set; }

        public string SuccessMessage { get; set; }
    }
}
