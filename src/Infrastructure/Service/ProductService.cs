using EShopOnRuleEngine.ConsoleApp.DTOs;
using EShopOnRuleEngine.ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Infrastructure.Service
{
    public class ProductService : IProductService
    {
        public ProductDto AddProductToStore(ProductDto newProduct)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get product data from static json file
        /// </summary>
        /// <returns>ProductDto</returns>
        public List<ProductDto> GetProductsFromStore()
        {
            // get the products from json file. Will get it from the db
            var productDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"SeedData\\productItems.json"}");
            var productsJson = System.IO.File.ReadAllText(productDirectory);
            var productsDto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDto>>(productsJson);
            return productsDto;

        }
    }
}
