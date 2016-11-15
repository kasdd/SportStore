using System;
using System.ComponentModel;

namespace SportsStore.Models.Domain
{
    public  class Product
    {
        public int ProductId { get;  set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [DisplayName("In stock")]
        public bool InStock { get; set; }
        public DateTime? AvailableTill { get; set; }
        public Availability Availability { get; set; }
        public virtual Category Category { get; set; }

        public Product()
        {
            Availability = Availability.ShopAndOnline;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Product)
                if ((obj as Product).ProductId == ProductId)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }

 
}