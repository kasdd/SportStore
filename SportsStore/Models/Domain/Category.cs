using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SportsStore.Models.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public void AddProduct(string naam, int price, string description)
        {
            if (Products.FirstOrDefault(p => p.Name == naam) == null)
            {
                Product p = new Product() {Description = description, Name = naam, Price = price, Category = this};
                Products.Add(p);
            }
        }

        public Product FindProduct(string naam)
        {
            return Products.Where(p => p.Name == naam).FirstOrDefault();
        }
    }
}
