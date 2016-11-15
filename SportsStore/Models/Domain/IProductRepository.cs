using System;
using System.Linq;

namespace SportsStore.Models.Domain
{
    public interface IProductRepository
    {
        IQueryable<Product> FindAll();
        Product FindById(int productId);
        void Add(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
