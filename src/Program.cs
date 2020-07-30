using EShopOnRuleEngine.ConsoleApp.Infrastructure.Service;
using EShopOnRuleEngine.ConsoleApp.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EShopOnRuleEngine.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Added DI to create instance of all available services
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IProductService, ProductService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("---------Available Products (Unit price for SKU IDs)---------");
            Console.WriteLine("SKU \t Price");
            var products = serviceProvider
                .GetService<IProductService>()
                .GetProductsFromStore();
            products?.ForEach(product =>
            {
                Console.WriteLine(product.SKU + "\t" + "Rs. " + product.Price);
            });

        }
    }
}
