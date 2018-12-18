using System.Collections.Generic;

namespace FinalCapstone.Models
{
    public class RemoveAToolViewModel
    {
        public IList<Tool> Tools { get; set; }

        public string SuccessMessage { get; set; }
    }
}
