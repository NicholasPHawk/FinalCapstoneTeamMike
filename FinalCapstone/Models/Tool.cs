using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Tool
    {
            public int Id { get; set; }
            public string Brand { get; set; }
            public string ToolName { get; set; }
            public string Description { get; set; }
            public bool CheckedOut { get; set; }
            public string ImageName
            {
                get => ImageName = ImageName;
                set
                {
                    ImageName = $"{Brand} {ToolName}";
                }
            }
    }
}
