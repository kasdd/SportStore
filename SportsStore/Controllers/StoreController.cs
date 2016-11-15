using System.Linq;
using System.Web.Mvc;
using SportsStore.Models.Domain;
using System;
using System.Collections.Generic;

namespace SportsStore.Controllers
{
    
    public class StoreController : Controller
    {

        private IProductRepository productRepository;

       
        public StoreController(IProductRepository productsRepository)
        {
            this.productRepository = productsRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<Product> products = productRepository.FindAll().Where(p => p.Availability != Availability.ShopOnly && (p.AvailableTill == null || p.AvailableTill >= DateTime.Today)).OrderBy(p => p.Name).ToList();
            return View(products);
        }

    }
}
