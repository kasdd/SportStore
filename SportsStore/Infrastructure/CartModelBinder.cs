using System.Web.Mvc;
using SportsStore.Models.Domain;

namespace SportsStore.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string cartSessionKey = "cart";
    
    public object  BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        
        Cart cart = controllerContext.HttpContext.Session[cartSessionKey] as Cart;
        if (cart == null)
        {
            cart = new Cart();
            controllerContext.HttpContext.Session[cartSessionKey] = cart;
        }
        return cart;
    }
}
}