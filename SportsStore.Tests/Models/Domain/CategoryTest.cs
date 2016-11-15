using System.Linq;
using SportsStore.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SportsStore.Tests.Models.Domain
{
    

    [TestClass()]
    public class CategoryTest
    {

        private Category category;
        private Product p1;
        private Product p2;

        [TestInitialize]
        public void Initialize()
        {
            p1 = new Product { ProductId = 1, Name="Football", Price = 10 };
            p2 = new Product { ProductId = 2, Name="short", Price = 5 };
            category = new Category();
            
        }

        [TestMethod]
        public void CategoryStartsEmpty()
        {
            Category cat = new Category();
            Assert.AreEqual(0, cat.Products.Count());
        }

        [TestMethod]
        public void AddProductSucceeds()
        {
            category.AddProduct("Football", 10, null);
            Assert.AreEqual(category.Products.Count(), 1);
        }

        [TestMethod]
        public void FindProductFootballSucceeds()
        {
            category.AddProduct("Football", 10, null);
            Assert.IsNotNull(category.FindProduct("Football"));
        }
    }
}
