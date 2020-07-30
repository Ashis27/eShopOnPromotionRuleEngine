using EShopOnRuleEngine.ConsoleApp.DTOs;
using EShopOnRuleEngine.ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Infrastructure.Service
{
    public class CheckoutService : ICheckoutService
    {
        private ICartService _cartService;
   
        private IProductService _productService;

        public CheckoutService(ICartService cartService,
       
            IProductService productService)
        {
            _cartService = cartService;
          
            _productService = productService;
           
        }

        /// <summary>
        /// Apply promo code and calculate price after discount.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public CartDto Checkout(CartDto cart)
        {
            var products = _productService.GetProductsFromStore();

            cart.CartItems.ForEach(item =>
            {
                if (!products.Select(p => p.SKU).Contains(item.SKU))
                {
                    throw new Exception("Invalid item added to Cart.");
                }
                item.UnitPrice = products.First(p => p.SKU == item.SKU).Price;
            });

        
            var cartDto = new CartDto
            {
                CartId = !string.IsNullOrEmpty(cart.CartId) ? cart.CartId : Guid.NewGuid().ToString(),
                CartItems = null
            };

            return cartDto;
        }
    }
}
