using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class CategoryMapper : EntityTypeConfiguration<Category>
    {
        public CategoryMapper()
        {
            ToTable("Category");

            Property(t => t.Name).IsRequired().HasMaxLength(100);

            HasMany(t => t.Products).WithRequired(t=>t.Category).Map(m => m.MapKey("CategoryId")).WillCascadeOnDelete(false);
        }
    }
}