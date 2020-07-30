using EShopOnRuleEngine.ConsoleApp.Infrastructure.Service;
using EShopOnRuleEngine.ConsoleApp.Interface;
using EShopOnRuleEngine.ConsoleApp.Interface.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                var serviceProvider = serviceCollection.BuildServiceProvider();

                Console.WriteLine("---------Available Products (Unit price for SKU IDs)---------");
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

                Console.WriteLine();
                Console.WriteLine("---------Active Promotions-------");
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
            catch (Exception ex)
            {
                Console.WriteLine("***Something went wrong. Please try agian***");
            }
        }
    }
}
