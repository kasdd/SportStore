using SportsStore.Infrastructure;
using SportsStore.Models.DAL;
using SportsStore.Models.Domain;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SportsStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            new SportsStoreContext().Categories.ToList();
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
