using Moq;
using SportsStore.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SportsStore.Models.Domain;
using System.Web.Mvc;

namespace SportsStore.Tests.Controllers
{

    [TestClass()]
    public class CartControllerTest
    {

        private CartController controller;
        private Cart cart;
        private Customer customer;
        private Mock<IProductRepository> productRepository;
        private DummyContext context;

        [TestInitialize]
        public void MyTestInitialize()
        {
            context = new DummyContext();
            productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.FindAll()).Returns(context.Products);
            productRepository.Setup(p => p.FindById(1)).Returns(context.Football);
            productRepository.Setup(p => p.FindById(4)).Returns(context.RunningShoes);
            controller = new CartController(productRepository.Object);
            cart = new Cart();
            cart.AddLine(context.Football, 2);
            customer = context.Customer;
        }

        #region Index
        [TestMethod()]
        public void IndexShowsEmptyCartWhenCartIsEmpty()
        {
            Cart emptycart = new Cart();
            ViewResult result = controller.Index(emptycart) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("EmptyCart", result.ViewName);
        }

        [TestMethod]
        public void IndexShowsCartWhenCartNotEmpty()
        {
            ViewResult result = controller.Index(cart) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(String.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void IndexWillShowCartWhenCartNotEmpty()
        {
            ViewResult result = controller.Index(cart) as ViewResult;
            Cart cartresult = result.ViewData.Model as Cart;
            Assert.AreEqual(1, cart.NumberOfItems);
        }
        #endregion

        #region Add
        [TestMethod]
        public void AddWillRedirectToStore()
        {
            RedirectToRouteResult result = controller.Add(4, 2, cart) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Store", result.RouteValues["controller"]);
            productRepository.Verify(p => p.FindById(4), Times.Once());
        }

        [TestMethod]
        public void AddWillAddProductToCart()
        {
            RedirectToRouteResult result = controller.Add(4, 2, cart) as RedirectToRouteResult;
            Assert.AreEqual(2, cart.NumberOfItems);
            productRepository.Verify(p => p.FindById(4), Times.Once());
        }

        #endregion

        #region Remove
        [TestMethod]
        public void RemoveWillRedirectToIndex()
        {
            RedirectToRouteResult result = controller.Remove(1, cart) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void RemoveWillRemoveProductFromCart()
        {
            RedirectToRouteResult result = controller.Remove(1, cart) as RedirectToRouteResult;
            Assert.AreEqual(0, cart.NumberOfItems);
            productRepository.Verify(p => p.FindById(1), Times.Once());
        }
        #endregion

        #region Plus
        [TestMethod]
        public void PlusWillRedirectToIndex()
        {
            RedirectToRouteResult result = controller.Plus(1, cart) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void PlusWillIncreaseQuantity()
        {
            RedirectToRouteResult result = controller.Plus(1, cart) as RedirectToRouteResult;
            CartLine line = cart.CartLines.ToList()[0];
            Assert.AreEqual(3, line.Quantity);
        }
        #endregion

        #region Min
        [TestMethod]
        public void MinWillRedirectToIndex()
        {
            RedirectToRouteResult result = controller.Min(1, cart) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void MinWillDecreaseQuantity()
        {
            RedirectToRouteResult result = controller.Min(1, cart) as RedirectToRouteResult;
            CartLine line = cart.CartLines.ToList()[0];
            Assert.AreEqual(1, line.Quantity);
        }
        #endregion

    }
}
