using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SportsStore.Models.Domain
{
    public  class Order
    {
        #region Properties
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Giftwrapping { get; set; }
        public string Message;
        public string ShippingStreet { get; set; }
        public virtual City ShippingCity { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; private set; }
        public decimal Total { get { return OrderLines.Sum(o=>o.Price * o.Quantity); } }

        #endregion

        #region Constructors
        public Order()
        {
            OrderLines = new List<OrderLine>();
     
        }

        public Order(Cart cart,  DateTime? deliveryDate, bool giftwrapping, string shippingStreet, City shippingCity): this()
        {
            if (cart.CartLines.Count()== 0)
            throw new ApplicationException("Cannot place order when cart is empty");

            foreach (CartLine line in cart.CartLines)
           OrderLines.Add(new OrderLine
                {
                     Product = line.Product,
                    Price = line.Product.Price,
                    Quantity = line.Quantity
                });

            OrderDate = DateTime.Today;
            DeliveryDate = deliveryDate;
            Giftwrapping = giftwrapping;
            ShippingStreet = shippingStreet;
            ShippingCity = shippingCity;
        } 
        #endregion

        #region Methods
        public bool HasOrdered(Product p)
        {
            foreach (OrderLine l in OrderLines)
                if (l.Product == p)
                    return true;
            return false;
        }
        #endregion

    }
}