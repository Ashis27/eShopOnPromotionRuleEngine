using EShopOnRuleEngine.ConsoleApp.DTOs;
using EShopOnRuleEngine.ConsoleApp.Infrastructure.Service;
using EShopOnRuleEngine.ConsoleApp.Interface;
using EShopOnRuleEngine.ConsoleApp.Interface.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShopOnRuleEngine.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Added DI to create instance of all available services
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSingleton<IProductService, ProductService>();
                serviceCollection.AddSingleton<ICartService, CartService>();
                serviceCollection.AddSingleton<IPromoRuleService, PromoRuleService>();
                serviceCollection.AddSingleton<ICheckoutService, CheckoutService>();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                Console.WriteLine("---------Available Products (Unit price for SKU IDs)---------");
                List<ProductDto> availableProducts = GetActiveProducts(serviceProvider);

                Console.WriteLine();
                Console.WriteLine("---------Active Promotions-------");
                GetActivePromototions(serviceProvider);

                Console.WriteLine();
                Console.WriteLine("Please add items to your cart(SKU Space Unit)");
                Console.WriteLine("------------------------------");
                Console.WriteLine("** Press c to checkout **");
                Console.WriteLine("** Press e to exit **");
                Console.WriteLine("------------------------------");
                ProceedToCheckOut(serviceProvider, availableProducts);


            }
            catch (Exception ex)
            {
                Console.WriteLine("***Something went wrong. Please try agian***");
            }
        }

        private static List<ProductDto> GetActiveProducts(ServiceProvider serviceProvider)
        {
            Console.WriteLine("SKU \t Price");
            var products = serviceProvider
                .GetService<IProductService>()
                .GetProductsFromStore();
            if (products != null && products.Count > 0)
            {
                products?.ForEach(product =>
                {
                    Console.WriteLine(product.SKU + "\t" + "Rs. " + product.Price);
                });
            }
            else
            {
                Console.WriteLine("***No Active promotion available***");
            }

            return products;
        }

        private static void GetActivePromototions(ServiceProvider serviceProvider)
        {
            var activePromoOffers = serviceProvider
                     .GetService<IPromoRuleService>()
                     .GetPromoRules()
                     .Where(promo => promo.ValidTill >= DateTime.Now)
                     .ToList();
            if (activePromoOffers != null && activePromoOffers.Count > 0)
            {
                activePromoOffers?.ForEach(promo =>
                {
                    Console.WriteLine(promo.PromotionOfferDescription);
                });
            }
            else
            {
                Console.WriteLine("***No Active promotion available***");
            }
        }

        private static void ProceedToCheckOut(ServiceProvider serviceProvider, List<ProductDto> products)
        {
            var checkoutService = serviceProvider
                .GetService<ICheckoutService>();

            List<CartItemDto> cartItems = new List<CartItemDto>();
            bool incorrectInput = false;

            Console.WriteLine("Add Item:");
            Console.WriteLine("Product \t Unit");
            while (true)
            {
                string line = Console.ReadLine();
                if (line?.Trim().ToLower() == "e".ToLower())
                {
                    break;
                }

                if (line?.Trim().ToLower() == "c".ToLower())
                {
                    // checkout
                    var result = checkoutService.Checkout(new CartDto { CartId = Guid.NewGuid().ToString(), CartItems = cartItems });
                    Console.WriteLine("SKU \t OfferPrice");
                    result.CartItems.ForEach(r =>
                    {
                        Console.WriteLine(r.SKU + "\t" + r.OfferPrice);
                    });
                    break;
                }

                var splittedInput = line.Split(" ");

                if (splittedInput.Length < 2)
                {
                    incorrectInput = true;
                }

                var productSku = splittedInput[0];
                var quantity = Convert.ToInt32(splittedInput[1]);
                var product = products.FirstOrDefault(p => p.SKU.ToLower() == productSku.ToLower());
                if (product != null)
                {
                    CartItemDto item = new CartItemDto { SKU = productSku, Quantity = quantity, UnitPrice = product.Price };
                    cartItems.Add(item);
                }
                else
                {
                    Console.WriteLine($"Invalid product item - {productSku} - added to cart");
                }
                
                
            }
            Console.ReadKey();
        }
    }
}
