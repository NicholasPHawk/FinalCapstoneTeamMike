using System.Collections.Generic;

namespace FinalCapstone.Models.Cart
{
    public class Cart
    {
        public IList<Tool> Tools { get; } = new List<Tool>();

        public void AddToCart(Tool tool)
        {
            Tools.Add(tool);
        }
    }
}