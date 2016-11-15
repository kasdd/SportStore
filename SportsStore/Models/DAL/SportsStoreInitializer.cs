using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class SportsStoreInitializer :
                     DropCreateDatabaseAlways<SportsStoreContext>
    {


        protected override void Seed(SportsStoreContext context)
        {
            try
            {


                City gent = new City {Name = "Gent", Postalcode = "9000"};
                City antwerpen = new City {Name = "Antwerpen", Postalcode = "3000"};
                var cities = new List<City>
                                 {
                                     gent,
                                     antwerpen
                                 };
                cities.ForEach(c => context.Cities.Add(c));

                Category watersports = new Category() {Name = "WaterSports"};
                Category soccer = new Category() {Name = "Soccer"};
                Category chess = new Category() {Name = "Chess"};
                var categories = new List<Category>
                                     {
                                         watersports,
                                         soccer,
                                         chess
                                     };

                categories.ForEach(c => context.Categories.Add(c));

                soccer.AddProduct("Football", 25, "WK colors");
                soccer.AddProduct("Corner flags", 34, "Give your playing field that professional touch");
                soccer.AddProduct("Stadium", 79500, "Flat-packed 35000-seat stadium");
                soccer.AddProduct("Running shoes", 95, "Protective and fashionable");
                watersports.AddProduct("Surf board", 275, "A boat for one person");
                watersports.AddProduct("Kayak", 170, "High quality");
                watersports.AddProduct("Lifejacket", 49, "Protective and fashionable");
                chess.AddProduct("Thinking cap", 16, "Improve your brain efficiency by 75%");
                chess.AddProduct("Unsteady chair", 30, "Secretly give your opponent a disadvantage");
                chess.AddProduct("Human chess board", 75, "A fun game for the whole extended family!");
                chess.AddProduct("Bling-bling King", 1200, "Gold plated, diamond-studded king");

                context.SaveChanges();

                Random r = new Random();
                for (int i = 1; i < 10; i++)
                {
                    Customer klant = new Customer
                                         {
                                             CustomerName = "student" + i,
                                             Name = "Student" + i,
                                             FirstName = "Jan",
                                             Email="Student" + i + "@hogent.be",
                                             Street = "Nieuwstraat 100",
                                             City = cities[r.Next(2)]
                                         };
                    context.Customers.Add(klant);
                    Cart cart = new Cart();
                    if (i%2 == 1)
                    {
                        cart.AddLine(soccer.FindProduct("Football"), 1);
                        cart.AddLine(soccer.FindProduct("Corner flags"), 2);
                        klant.PlaceOrder(cart, DateTime.Today.AddDays(10), false, "Nieuwstraat 10", klant.City);
                        context.SaveChanges();
                    }
                }
         
            }

            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                   s+=String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }
        
    }
    }
