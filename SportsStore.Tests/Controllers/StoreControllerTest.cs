using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using System.Web.Mvc;
using SportsStore.Models.Domain;

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class StoreControllerTest
    {
        private StoreController controller;
        private Mock<IProductRepository> productRepository;
        [TestInitialize]
        public void MyTestInitialize()
        {
            DummyContext context = new DummyContext();
            productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.FindAll()).Returns(context.Products);
            controller = new StoreController(productRepository.Object);
        }
        [TestMethod]
        public void IndexUsesConventionToChooseView()
        {
            //Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void IndexWillShowProducts()
        {
            ViewResult result = controller.Index() as ViewResult;
            IList<Product> products = result.ViewData.Model as IList<Product>;
            Assert.AreEqual(11, products.Count);
        }
    }
}
