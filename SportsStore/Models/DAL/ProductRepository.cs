using System.Data.Entity;
using System.Linq;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class ProductRepository:IProductRepository
    {
        private SportsStoreContext context ;
        private readonly DbSet<Product> products;

        public ProductRepository(SportsStoreContext context)
        {
            this.context = context;
            products = context.Products;
        }

        public IQueryable<Product> FindAll()
        {
            return products.OrderBy(p => p.Name);
        }

        public Product FindById(int productId)
        {
            return products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Delete(Product product)
        {
            products.Remove(product);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}