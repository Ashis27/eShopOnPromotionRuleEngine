using EShopOnRuleEngine.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Interface.Service
{
    public interface IPromoRuleService
    {
        List<PromoOffer> GetPromoRules();
    }
}
