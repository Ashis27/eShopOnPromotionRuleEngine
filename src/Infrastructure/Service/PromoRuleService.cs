using EShopOnRuleEngine.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Interface.Service
{
    public class PromoRuleService : IPromoRuleService
    {

        /// <summary>
        /// Promotion rule engine configuration
        /// </summary>
        /// <returns></returns>
        public List<PromoOffer> GetPromoRules()
        {
            var promoRuleDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"SeedData\\promotionRuleEnginee.json"}");
            var promoRuleJson = System.IO.File.ReadAllText(promoRuleDirectory);
            var promoOffers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PromoOffer>>(promoRuleJson);
            return promoOffers;
        }
    }
}
