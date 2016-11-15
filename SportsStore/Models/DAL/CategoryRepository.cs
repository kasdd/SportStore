using System.Data.Entity;
using System.Linq;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class CategoryRepository:ICategoryRepository
    {
        private SportsStoreContext context ;
        private readonly DbSet<Category> categories;

        public CategoryRepository(SportsStoreContext context)
        {
            this.context = context;
            categories = context.Categories;
        }

        public IQueryable<Category> FindAll()
        {
            return categories.OrderBy(p => p.Name);
        }

        public Category FindById(int categoryId)
        {
            return categories.Find(categoryId);
        }

    }
}