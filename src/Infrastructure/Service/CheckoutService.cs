using EShopOnPromotionEngineeRule.API;
using EShopOnRuleEngine.ConsoleApp.DTOs;
using EShopOnRuleEngine.ConsoleApp.Interface;
using EShopOnRuleEngine.ConsoleApp.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Infrastructure.Service
{
    public class CheckoutService : ICheckoutService
    {
        private ICartService _cartService;
        private IPromoRuleService _promoRuleService;
        private PromoOfferManager _promoCalculator;
        private IProductService _productService;

        public CheckoutService(ICartService cartService,
            IPromoRuleService promoRuleService,
            IProductService productService)
        {
            _cartService = cartService;
            _promoRuleService = promoRuleService;
            _productService = productService;
            _promoCalculator = PromoOfferManager.Instance;
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

            var promoOffers = _promoRuleService.GetPromoRules();
            var cartItemsWithOfferPrice = _promoCalculator.CalculateOfferPrice(_promoCalculator.ApplyPromoRule(cart.CartItems, promoOffers), promoOffers);
           
            var cartDto = new CartDto
            {
                CartId = !string.IsNullOrEmpty(cart.CartId) ? cart.CartId : Guid.NewGuid().ToString(),
                CartItems = cartItemsWithOfferPrice
            };

            return cartDto;
        }
    }
}
