using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class CustomerMapper : EntityTypeConfiguration<Customer>
    {
        public CustomerMapper()
        {
            ToTable("Customer");
            HasKey(t => t.CustomerName);
            Property(t => t.CustomerName).HasMaxLength(20);
            Property(t => t.Name).IsRequired().HasMaxLength(100);
            Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            Property(t => t.Street).HasMaxLength(100);

            HasMany(t => t.Orders).WithRequired().Map(m => m.MapKey("CustomerName")).WillCascadeOnDelete(true);
            HasRequired(t => t.City).WithMany().Map(m => m.MapKey("PostalCode")).WillCascadeOnDelete(false);

        }
    }
}