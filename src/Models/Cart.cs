using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Models
{
    public class Cart
    {
        public Guid CartId { get; private set; }
        public List<CartItem> CartItems { get; private set; }

        public static Cart CreateCart(List<CartItem> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
                throw new Exception("Atleast one item needs to be added to cart.");
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                CartItems = new List<CartItem>(cartItems)
            };
            return cart;
        }

        public void AddItemToCart(CartItem cartItem)
        {
            if (cartItem.Quantity < 1)
                throw new Exception("Quantity for cart item can not be less than 1.");
            this.CartItems.Add(cartItem);
        }

        public void RemoveItemFromCart(CartItem cartItem)
        {
            if (!this.CartItems.Any(p => p.SKU == cartItem.SKU))
                throw new Exception("Item not found in cart.");
        }
    }
}
