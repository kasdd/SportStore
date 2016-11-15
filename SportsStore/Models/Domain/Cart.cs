using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.Domain
{
    public class Cart
    {
        #region Properties
        private IList<CartLine> lines = new List<CartLine>();
        public IEnumerable<CartLine> CartLines { get { return lines.AsEnumerable(); } }
        public int NumberOfItems { get { return lines.Count(); } }
        public decimal TotalValue
        {
            get { return lines.Sum(l => l.Product.Price * l.Quantity); }
        }
        #endregion

        #region Methods
        public void AddLine(Product product, int quantity)
        {
            CartLine line = lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line == null)
                lines.Add(new CartLine { Product = product, Quantity = quantity });
            else line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            CartLine line = GetCartLine(product.ProductId);
            if (line != null)
                lines.Remove(line);
        }

        public void IncreaseQuantity(int productId)
        {
            CartLine line = GetCartLine(productId);
            if (line != null)
                line.Quantity++;
        }

        public void DecreaseQuantity(int productId)
        {
            CartLine line = GetCartLine(productId);
            if (line != null)
                line.Quantity--;
            if (line.Quantity<=0)
                lines.Remove(line);
        }

        private CartLine GetCartLine(int productId)
        {
            return lines.SingleOrDefault(l => l.Product.ProductId == productId);
        }

        public void Clear()
        {
            lines.Clear();
        }
        
        #endregion

     
    }
}