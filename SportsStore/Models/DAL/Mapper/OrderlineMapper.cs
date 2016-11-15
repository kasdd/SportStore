using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class OrderlineMapper : EntityTypeConfiguration<OrderLine>
    {
        public OrderlineMapper()
        {
            ToTable("OrderLine");
            HasKey(t => new { t.OrderId, t.ProductId });

            HasRequired(t => t.Product).WithMany().HasForeignKey(t => t.ProductId).WillCascadeOnDelete(false);
        }
    }
}