using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Controllers;
using SportsStore.Models.Domain;
using Moq;
using SportsStore.ViewModels;

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        private ProductController productController;
        private Mock<IProductRepository> mockProductRepository;
        private Mock<ICategoryRepository> mockCategoryRepository;
        private readonly DummyContext dummyContext = new DummyContext();
        private Product runningShoes;
        private int runningShoesId;
        private Product nieuwProduct;

        [TestInitialize]
        public void MyTestInitializer()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mockCategoryRepository = new Mock<ICategoryRepository>();
            runningShoes = dummyContext.RunningShoes;
            runningShoesId = runningShoes.ProductId;
            mockProductRepository.Setup(p => p.FindAll()).Returns(dummyContext.Products);
            mockProductRepository.Setup(p => p.FindById(4)).Returns(dummyContext.RunningShoes);
            mockProductRepository.Setup(p => p.Add(It.IsNotNull<Product>()));
            mockProductRepository.Setup(p => p.Delete(dummyContext.RunningShoes));
            mockProductRepository.Setup(p => p.SaveChanges());
            mockCategoryRepository.Setup(c => c.FindAll()).Returns(dummyContext.Categories);
            //extra voor delete
            mockProductRepository.Setup(p => p.FindById(1)).Returns(dummyContext.Football);
            mockProductRepository.Setup(p => p.Delete(dummyContext.Football)).Throws<Exception>();
            productController = new ProductController(mockProductRepository.Object, mockCategoryRepository.Object);
            nieuwProduct =
            new Product()
            {
                ProductId = 100,
                Availability = Availability.OnlineOnly,
                Category = dummyContext.Categories.First(),
                Description = "nieuw product",
                Name = "nieuw product",
                Price = 10
            };
        }

        #region == Index ==

        [TestMethod]
        public void IndexRetourneertAlleProductenGesorteerdOpNaam()
        {
            ViewResult result = productController.Index() as ViewResult;
            List<Product> products = (result.Model as IEnumerable<Product>).ToList();
            Assert.AreEqual(11, products.Count);
            Assert.AreEqual("Bling-bling King", products[0].Name);
            Assert.AreEqual("Unsteady chair", products[10].Name);
        }

        #endregion

        #region == Edit ==

        [TestMethod]
        public void EditHttpGetRetourneertEenProductViewModelVoorHetProduct()
        {
            ViewResult result = productController.Edit(runningShoesId) as ViewResult;
            ProductViewModel productVm = result.Model as ProductViewModel;
            Assert.AreEqual(runningShoesId, productVm.ProductId);
        }

        [TestMethod]
        public void EditHttpGetRetourneertEenSelectListMetAlleCategoriënEnGeselecteerdeValue()
        {
            ViewResult result = productController.Edit(runningShoesId) as ViewResult;
            SelectList categories = (result.ViewBag.Categories as SelectList);
            Assert.AreEqual(dummyContext.Categories.Count(), categories.Count());
            Assert.AreEqual(1, categories.SelectedValue);
        }

        [TestMethod]
        public void EditHttpGetRetourneertHttpNotFoundVoorOnbestaandProduct()
        {
            ActionResult result = productController.Edit(-1);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditHttpPostWijzigtHetProduct()
        {
            ProductViewModel productVm = new ProductViewModel(runningShoes);
            productVm.Name = "RunningShoesGewijzigd";
            productVm.Price = 1000;
            productController.Edit(runningShoesId, productVm);
            Assert.AreEqual("RunningShoesGewijzigd", runningShoes.Name);
            Assert.AreEqual(1000, runningShoes.Price);
            Assert.AreEqual("Protective and fashionable", runningShoes.Description);
            mockProductRepository.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void EditHttpPostRedirectNaarIndex()
        {
            ProductViewModel productVm = new ProductViewModel(runningShoes);
            RedirectToRouteResult result = productController.Edit(runningShoesId, productVm) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void EditHttpPostWijzigtProductNietBijOngeldigModel()
        {
            ProductViewModel productVm = new ProductViewModel(runningShoes);
            productVm.Price = -1;
            productController.ModelState.AddModelError("key", "errorMessage");
            ViewResult result = productController.Edit(runningShoesId, productVm) as ViewResult;
            Assert.AreEqual(95, runningShoes.Price);
            mockProductRepository.Verify(m => m.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void EditHttpPostGeeftOngewijzigdVmOpnieuwDoorAanViewBijOngeldigModel()
        {
            ProductViewModel productVm = new ProductViewModel(runningShoes);
            productVm.Price = -1;
            productController.ModelState.AddModelError("key", "errorMessage");
            ViewResult result = productController.Edit(runningShoesId, productVm) as ViewResult;
            Assert.AreEqual(productVm, result.Model as ProductViewModel);
        }

        [TestMethod]
        public void EditHttpPostGeeftSelectListDoorAanViewBijOngeldigModel()
        {
            ProductViewModel productVm = new ProductViewModel(runningShoes);
            productVm.Price = -1;
            productController.ModelState.AddModelError("key", "errorMessage");
            ViewResult result = productController.Edit(runningShoesId, productVm) as ViewResult;
            Assert.AreEqual(dummyContext.Categories.Count(), (result.ViewBag.Categories as SelectList).ToArray().Length);
        }

        #endregion

        #region == Create ==

        [TestMethod]
        public void CreateHttpGetRetourneertEenProductViewModelVoorEenNieuwProduct()
        {
            ViewResult result = productController.Create() as ViewResult;
            ProductViewModel productVm = result.Model as ProductViewModel;
            Assert.IsNull(productVm.Name);
            Assert.AreEqual(0, productVm.ProductId);
        }

        [TestMethod]
        public void CreateHttpPostRedirectNaarIndex()
        {
            ProductViewModel productVm = new ProductViewModel(nieuwProduct);
            RedirectToRouteResult result = productController.Create(productVm) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void CreateHttpPostVoegtNieuweProductToe()
        {
            ProductViewModel productVm = new ProductViewModel(nieuwProduct);
            productController.Create(productVm);
            mockProductRepository.Verify(m => m.Add(nieuwProduct), Times.Once);
            mockProductRepository.Verify(m => m.SaveChanges(), Times.Once);
        }

       [TestMethod]
        public void CreateHttpPostVoegtGeenProductToeBijOngeldigModel()
       {
           nieuwProduct.Price = 0;
            ProductViewModel productVm = new ProductViewModel(nieuwProduct);
            productController.ModelState.AddModelError("key", "errorMessage");
            productController.Create(productVm);
            mockProductRepository.Verify(m => m.Add(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void CreateHttpPostGeeftOngewijzigdVmOpnieuwDoorBijOngeldigModel()
        {
            nieuwProduct.Price = 0;
            ProductViewModel productVm = new ProductViewModel(nieuwProduct);
            productController.ModelState.AddModelError("key", "errorMessage");
            ViewResult result = productController.Create(productVm) as ViewResult;
            Assert.AreEqual(productVm, result.Model as ProductViewModel);
        }
        #endregion

        #region == Delete ==

        [TestMethod]
        public void DeleteHttpGetRetourneertEenProduct()
        {
            ViewResult result = productController.Delete(runningShoesId) as ViewResult;
            Product productVm = result.Model as Product;
            Assert.AreEqual("Running shoes", productVm.Name);
        }

        [TestMethod]
        public void DeleteHttpRetourneertHttpNotFound()
        {
            ActionResult result = productController.Delete(-1);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteHttpPostVerwijdertProduct()
        {
            productController.DeleteConfirmed(runningShoesId);
            mockProductRepository.Verify(m => m.Delete(runningShoes), Times.Once);
            mockProductRepository.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteHttpPostRedirectNaarIndex()
        {
            RedirectToRouteResult result = productController.DeleteConfirmed(runningShoesId) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

 	[TestMethod]
        public void DeleteHttpPostRedirectNaarIndexMetTempDataAlsFaalt()
        {
            RedirectToRouteResult result = productController.DeleteConfirmed(dummyContext.Football.ProductId) as RedirectToRouteResult;
 	         Assert.IsNotNull(productController.TempData["error"]);
        }
        #endregion


    }

}
