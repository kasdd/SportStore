using System;
using System.Collections.Generic;


namespace SportsStore.Models.Domain
{
    public class Customer
    {
        #region Properties
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Order> Orders { get; private set; }
        #endregion

        #region Methods

        public Customer()
        {
            Orders = new List<Order>();
        }


        public void PlaceOrder( Cart cart, DateTime? deliveryDate, bool giftwrapping, string shippingStreet, City shippingCity)
        {
            Orders.Add(new Order(cart, deliveryDate, giftwrapping, shippingStreet, shippingCity));
        }
        #endregion
    }
}