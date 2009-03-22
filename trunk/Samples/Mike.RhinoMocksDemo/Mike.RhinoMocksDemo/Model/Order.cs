using System.Collections.Generic;
using System.Linq;

namespace Mike.RhinoMocksDemo.Model
{
    public class Order
    {
        public int Id { get; set; }

        private readonly IList<OrderLine> orderLines = new List<OrderLine>();
        public OrderLine[] OrderLines
        {
            get { return orderLines.ToArray(); }
        }

        public void AddOrderLine(int quantity, Product product)
        {
            orderLines.Add(new OrderLine
            {
                Quantity = quantity,
                Product = product
            });
        }
    }
}