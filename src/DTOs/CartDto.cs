using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.DTOs
{
    public class CartDto
    {
        public string CartId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(p => p.OfferPrice);
            }
        }
    }
}
