using System.Web.Mvc;
using SportsStore.Models.Domain;


namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
       private IProductRepository productRepository;
    
        public CartController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

         public ActionResult Index(Cart cart)
        {
            if (cart.NumberOfItems == 0)
                return View("EmptyCart");
            ViewBag.Total = cart.TotalValue;      
            return View(cart.CartLines);
        }

        public ActionResult Add(int id, int quantity, Cart cart)
        {
            Product product = productRepository.FindById(id);
            if (product != null)
            {
               cart.AddLine(product, quantity);
                TempData["Info"] = "Product " + product.Name + " has been added to the cart";
            }
            return RedirectToAction("Index", "Store");
        }

      
        public ActionResult Remove(int id, Cart cart)
        {
            Product product = productRepository.FindById(id);
            cart.RemoveLine(product);
            return RedirectToAction("Index");
        }

        public ActionResult Plus(int id, Cart cart)
        {
            cart.IncreaseQuantity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Min(int id, Cart cart)
        {
           cart.DecreaseQuantity(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles="customer")]
        public ActionResult CheckOut(Cart cart)
        {
            return View();
        }

    }
}
