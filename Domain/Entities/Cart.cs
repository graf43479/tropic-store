using System.Collections.Generic;
using System.Linq;


namespace Domain.Entities
{
    public class Cart
    {
       private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity , int userID)
        {
            /*CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();*/
            CartLine line = lineCollection
                .FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            if (line == null)
            {
                lineCollection.Add(new CartLine
                    {
                        Product = product,
                        Quantity = quantity,
                        UserID = userID
                    } );
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product, int quantity, int userID)
        {
            CartLine line = lineCollection
                .FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            
            if (line == null)
            {
                lineCollection.Remove(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    UserID = userID
                });
            }
            else
            {
                line.Quantity -= quantity;
            }
        }


        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price*e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
 
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

       /* public DimShipping DimShipping { get; set; }
        public int ShippingID { get; set; }*/
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int UserID {get; set;}
    }
    
}
