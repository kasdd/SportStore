using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class OrderMapper : EntityTypeConfiguration<Order>
    {
        public OrderMapper()
        {
            ToTable("Order");

            Property(o => o.ShippingStreet).IsRequired().HasMaxLength(100);

            HasMany(t => t.OrderLines).WithRequired().HasForeignKey(t => t.OrderId).WillCascadeOnDelete(true);
            HasRequired(t => t.ShippingCity).WithMany().Map(m => m.MapKey("PostalCode")).WillCascadeOnDelete(false);
        }
    }
}