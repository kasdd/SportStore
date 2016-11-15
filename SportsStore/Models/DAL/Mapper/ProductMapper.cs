using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            ToTable("Product");
            HasKey(t => t.ProductId);

            Property(t=>t.Name).IsRequired().HasMaxLength(100);
        }
    }
}