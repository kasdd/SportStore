using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Models.Domain;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index(int categoryId = 0)
        {
            IEnumerable<Product> products;
            Category category;
            if (categoryId == 0)
                products = productRepository.FindAll().OrderBy(b => b.Name).ToList();
            else
            {
                category = categoryRepository.FindById(categoryId);
                products = category.Products.OrderBy(b => b.Name);
            }
            if (Request.IsAjaxRequest())
                return PartialView("Products", products);
            ViewBag.Categories = GetCategoriesSelectList(categoryId);
            ViewBag.CategoryId = categoryId;
            return View(products);
        }

        public ActionResult Edit(int id)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            ViewBag.Categories = GetCategoriesSelectList(product.Category.CategoryId);
            return View(new ProductViewModel(product));
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel productViewModel)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    MapToProduct(productViewModel, product);
                    productRepository.SaveChanges();
                    TempData["info"] = $"Product {product.Name} werd aangepast";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Categories = GetCategoriesSelectList(productViewModel.CategoryId);
            return View(productViewModel);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            ViewBag.Categories = GetCategoriesSelectList();
            return View("Edit", new ProductViewModel(product));
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product();
                    productRepository.Add(product);
                    MapToProduct(productViewModel, product);
                    productRepository.SaveChanges();
                    TempData["info"] = $"Product {product.Name} werd gecreëerd";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Categories = GetCategoriesSelectList(productViewModel.CategoryId);
            return View("Edit",productViewModel);
        }

        public ActionResult Delete(int id)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = productRepository.FindById(id);
                if (product == null)
                    return HttpNotFound();
                productRepository.Delete(product);
                productRepository.SaveChanges();
                TempData["info"] = $"Product { product.Name} werd verwijderd";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Verwijderen product mislukt. Probeer opnieuw. " +
                           "Als de problemen zich blijven voordoen, contacteer de  administrator.";
            }
            return RedirectToAction("Index");
        }

        private SelectList GetCategoriesSelectList(int selectedValue = 0)
        {
            return new SelectList(categoryRepository.FindAll().OrderBy(g => g.Name),
                "CategoryId", "Name", selectedValue);
        }

        private void MapToProduct(ProductViewModel productViewModel, Product product)
        {
            product.ProductId = productViewModel.ProductId;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;
            product.InStock = productViewModel.InStock;
            product.Availability = productViewModel.Availability;
            product.AvailableTill = productViewModel.AvailableTill;
            product.Category = categoryRepository.FindById(productViewModel.CategoryId);
        }

    }
}