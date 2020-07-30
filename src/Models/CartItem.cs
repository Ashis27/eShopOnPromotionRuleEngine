using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopOnRuleEngine.ConsoleApp.Models
{
    public class CartItem
    {
        public string SKU { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal OfferPrice { get; private set; }
        public decimal ActualPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
        public bool PromoApplied { get; private set; }
        public string PromoId { get; private set; }

        public CartItem(string sku, decimal unitPrice, int quantity)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
