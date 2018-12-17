using System.Collections.Generic;

namespace FinalCapstone.Models
{
    public class Cart
    {
        public IList<CartItem> Items { get; set; } 

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void AddToCart(Tool tool)
        {
            CartItem cartItem = new CartItem() { Tool = tool };
            Items.Add(cartItem);
        }
    }
}