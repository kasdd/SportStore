using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class CityMapper : EntityTypeConfiguration<City>
    {
        public CityMapper()
        {
            ToTable("City");
            HasKey(t => t.Postalcode);

            Property(t => t.Postalcode).IsFixedLength().HasColumnType("char").HasMaxLength(4);
            Property(t => t.Name).IsRequired().HasMaxLength(100);

        }
    }
}