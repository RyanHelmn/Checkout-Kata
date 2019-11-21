using System.Collections.Generic;
using System.Linq;
using CheckoutKata.Core.Extensions;
using CheckoutKata.Core.Interfaces;

namespace CheckoutKata.Core.Models
{
    public class Checkout : ICheckout
    {
        private readonly List<Product> _products;
        private readonly List<Discount> _discounts;
        private char[] _scannedProducts;

        public Checkout(List<Product> products, List<Discount> discounts)
        {
            _products = products;
            _discounts = discounts;
            _scannedProducts = new char[] { };
        }

        public void Scan(string skus)
        {
            if (skus.IsNullOrWhiteSpace())
            {
                return;
            }

            // Split on each char, populate _scannedProducts if product exists in our products
            _scannedProducts = skus
                .ToCharArray()
                .Where(sku => _products.Any(p => p.Sku.EqualsIgnoreCase(sku)))
                .ToArray();
        }

        public int GetTotalPrice()
        {
            var total = _scannedProducts.Sum(GetPrice);
            var discount = _discounts.Sum(GetDiscount);

            // Minus the discount from the original sum to get our total price
            return total - discount;
        }

        private int GetDiscount(Discount discount)
        {
            // If the amount per product is valid for discount, workout the discount value
            var itemCount = _scannedProducts.Count(p => p == discount.Sku);

            // 3xA = 130, 2xB = 45
            return itemCount / discount.Quantity * discount.Value;
        }

        private int GetPrice(char sku)
        {
            return _products.First(x => x.Sku.EqualsIgnoreCase(sku)).Price;
        }
    }
}