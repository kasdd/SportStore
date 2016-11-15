using System.Linq;
using SportsStore.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.Tests.Models.Domain
{


    [TestClass()]
    public class CartTest
    {

            private Cart cart;
            private Product p1;
            private Product p2;

            [TestInitialize]
            public void Initialize()
            {
                p1 = new Product { ProductId = 1, Price = 10 };
                p2 = new Product { ProductId = 2, Price = 5 };
                cart = new Cart();
            }

            [TestMethod]
            public void CartStartsEmpty()
            {
                Assert.AreEqual(0, cart.CartLines.Count());
            }

            [TestMethod]
            public void CartProductsCanBeAdded()
            {
                cart.AddLine(p1, 1);
                cart.AddLine(p2, 10);
                Assert.AreEqual(cart.NumberOfItems, 2);
                Assert.AreEqual(cart.CartLines.First(l => l.Product.Equals(p1)).Quantity, 1);
                Assert.AreEqual(cart.CartLines.First(l => l.Product.Equals(p2)).Quantity, 10);
            }

            [TestMethod]
            public void CartCombinesLinesWithSameProduct()
            {
                cart.AddLine(p1, 1);
                cart.AddLine(p2, 10);
                cart.AddLine(p1, 3);
                Assert.AreEqual(cart.NumberOfItems, 2);
                Assert.AreEqual(cart.CartLines.First(l => l.Product.Equals(p1)).Quantity, 4);
                Assert.AreEqual(cart.CartLines.First(l => l.Product.Equals(p2)).Quantity, 10);
            }

            [TestMethod]
            public void CartProductCanBeDeleted()
            {
                cart.AddLine(p1, 1);
                cart.AddLine(p2, 10);
                cart.RemoveLine(p2);
                Assert.AreEqual(cart.NumberOfItems, 1);
                Assert.AreEqual(cart.CartLines.First(l => l.Product.Equals(p1)).Quantity, 1);
            }

            [TestMethod]
            public void CartCanBeCleared()
            {
                cart.AddLine(p1, 1);
                cart.AddLine(p2, 10);
                cart.AddLine(p1, 3);
                cart.Clear();
                Assert.AreEqual(cart.NumberOfItems, 0);
            }

            [TestMethod]
            public void CartTotalValueIsSumofPriceTimesQuantity()
            {
                cart.AddLine(p1, 1);
                cart.AddLine(p2, 10);
                cart.AddLine(p1, 3);
                Assert.AreEqual(cart.TotalValue, 90);
            }
        }
    }
