using System.Collections.Generic;
using System.Linq;
using CheckoutKata.Core.Extensions;
using CheckoutKata.Core.Interfaces;

namespace CheckoutKata.Core.Models
{
    public class Checkout : ICheckout
    {
        private readonly List<Discount> _discounts;
        private readonly List<Product> _products;
        private char[] _scannedProducts;

        public Checkout(List<Discount> discounts, List<Product> products)
        {
            _discounts = discounts;
            _products = products;
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
            return total;
        }

        private int GetPrice(char sku)
        {
            return _products.First(x => x.Sku.EqualsIgnoreCase(sku)).Price;
        }
    }
}