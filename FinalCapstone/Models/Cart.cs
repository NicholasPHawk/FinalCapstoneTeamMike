using System.Collections.Generic;
using System.Linq;

namespace FinalCapstone.Models
{
    public class Cart
    {
        public IList<Tool> Tools { get; set; }

        public Cart()
        {
            Tools = new List<Tool>();
        }

        public void AddToCart(Tool tool)
        {
            Tools.Add(tool);
        }

        public void RemoveFromCart(int id)
        {
            Tool tool = Tools.FirstOrDefault(t => t.Id == id);
            Tools.Remove(tool);
        }
    }
}