using EShopOnRuleEngine.ConsoleApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Interface
{
    public interface ICheckoutService
    {
        CartDto Checkout(CartDto cart);
    }
}
