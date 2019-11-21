using System.Collections.Generic;
using CheckoutKata.Core.Interfaces;
using CheckoutKata.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKata.Tests
{
    [TestClass]
    public class CheckoutTests
    {
        private readonly ICheckout _checkout;

        public CheckoutTests()
        {
            var products = new List<Product>
            {
                new Product {Sku = 'A', Price = 50},
                new Product {Sku = 'B', Price = 30},
                new Product {Sku = 'C', Price = 20},
                new Product {Sku = 'D', Price = 15}
            };

            var discounts = new List<Discount>
            {
                new Discount {Sku = 'A', Quantity = 3, Value = 130},
                new Discount {Sku = 'B', Quantity = 2, Value = 45}
            };

            _checkout = new Checkout(products, discounts);
        }

        [TestMethod]
        [DataRow("A", 50)]
        [DataRow("AA", 100)]
        [DataRow("B", 30)]
        [DataRow("DDDD", 60)]
        public void ProductsAreCorrectPrice(string skus, int expectedTotal)
        {
            _checkout.Scan(skus);
            Assert.AreEqual(expectedTotal, _checkout.GetTotalPrice());
        }

        [TestMethod]
        [DataRow("AAA", 20)]
        [DataRow("BB", 15)]
        [DataRow("AAABB", 35)]
        [DataRow("ABABA", 35)]
        public void DiscountsAreCorrectTotal(string skus, int expectedTotal)
        {
            _checkout.Scan(skus);
            Assert.AreEqual(expectedTotal, _checkout.GetTotalPrice());
        }
    }
}
