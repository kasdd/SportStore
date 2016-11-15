using System.Collections.Generic;
using System.Linq;
using SportsStore.Models.Domain;

namespace SportsStore.Tests.Controllers
{
    public class DummyContext
    {
        private City gent;
        private City antwerpen;

        private Category watersports;
        private Category soccer;
        private Category chess;
        private IList<Product> products;
        
        public DummyContext()
        {
            products=new List<Product>();
            gent = new City { Name = "Gent", Postalcode = "9000" };
            antwerpen = new City { Name = "Antwerpen", Postalcode = "3000" };
           watersports = new Category() { Name = "WaterSports" };
             soccer = new Category() { CategoryId=1, Name = "Soccer" };
            chess = new Category() { CategoryId = 2, Name = "Chess" };
            soccer.AddProduct("Football", 25, "WK colors");
           Football.ProductId = 1;
            soccer.AddProduct("Corner flags", 34, "Give your playing field that professional touch");
            soccer.AddProduct("Stadium", 79500, "Flat-packed 35000-seat stadium");
            soccer.AddProduct("Running shoes", 95, "Protective and fashionable");
            RunningShoes.ProductId = 4;
            watersports.AddProduct("Surf board", 275, "A boat for one person");
            watersports.AddProduct("Kayak", 170, "High quality");
            watersports.AddProduct("Lifejacket", 49, "Protective and fashionable");
            chess.AddProduct("Thinking cap", 16, "Improve your brain efficiency by 75%");
            chess.AddProduct("Unsteady chair", 30, "Secretly give your opponent a disadvantage");
            chess.AddProduct("Human chess board", 75, "A fun game for the whole extended family!");
            chess.AddProduct("Bling-bling King", 1200, "Gold plated, diamond-studded king");
            foreach (Category c in Categories)
                foreach (Product p in c.Products)
                    products.Add(p);
        }
       
        public IQueryable<Category> Categories
        {
            get
            {
                return new List<Category>
                       {
                           watersports,
                           soccer,
                           chess
                       }.AsQueryable();
            }
        }

            public IQueryable<City> Cities
        {
            get
            {
                return new List<City> { gent, antwerpen }.AsQueryable();
            }
        }

        public Customer Customer
        {
            get
            {
                return new Customer { CustomerName = "jan", Name = "Janneman", FirstName = "Jan", Street = "Nieuwstraat 100", City = gent };
            }
        }

        public IQueryable<Product> Products { get { return products.AsQueryable(); } }

        public Product Football
        {
            get { return soccer.FindProduct("Football"); }
        }

        public Product RunningShoes
        {
            get { return soccer.FindProduct("Running shoes"); }
        }

        }
}
