using SportsStore.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.Tests.Models.Domain
{
    
  
    [TestClass()]
    public class OrderTest
    {

        private Order order;
        private Product p1;
        private Product p2;
        private Product p3;

        [TestInitialize]
        public void Initialize()
        {
            Cart cart = new Cart();
            p1 = new Product { ProductId = 1, Name = "Football", Price = 10 };
            p2 = new Product { ProductId = 2, Name = "short", Price = 5 };
            p3 = new Product { ProductId = 3, Name = "NotInOrder", Price = 5 };
            cart.AddLine(p1, 1);
            cart.AddLine(p2, 10);
            order = new Order(cart, null, false, "Teststraat 10", new City(){Postalcode = "3000", Name="Gent"});
        }

        [TestMethod]
        public void TotalReturns60()
        {
               Assert.AreEqual(60, order.Total);
        }
        [TestMethod]
        public void HasOrderedProductInOrderReturnsTrue()
        {
            Assert.IsTrue(order.HasOrdered(p1));
        }

        [TestMethod]
        public void HasOrderedProductNotInOrderReturnsFalse()
        {
            Assert.IsFalse(order.HasOrdered(p3));
        }
     
    }
}
