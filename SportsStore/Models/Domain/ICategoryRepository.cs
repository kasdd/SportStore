using System.Linq;

namespace SportsStore.Models.Domain
{
    public interface ICategoryRepository
    {
        IQueryable<Category> FindAll();
        Category FindById(int categoryId);
    }
}
