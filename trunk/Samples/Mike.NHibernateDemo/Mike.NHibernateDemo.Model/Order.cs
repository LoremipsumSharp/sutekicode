using System;
using System.Linq;
using System.Collections.Generic;

namespace Mike.NHibernateDemo.Model
{
    public class Order
    {
        private IList<OrderLine> orderLines = new List<OrderLine>();

        public IList<OrderLine> OrderLines
        {
            get { return orderLines; }
            set { orderLines = value; }
        }

        public DateTime OrderDate { get; set; }

        public void AddOrderLine(OrderLine orderLine)
        {
            orderLine.Order = this;
            orderLines.Add(orderLine);
        }

        public void AddProduct(Product product)
        {
            var orderLine = orderLines.SingleOrDefault(line => line.Product.Name == product.Name);
            if(orderLine == null)
            {
                AddOrderLine(new OrderLine
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                orderLine.Quantity++;
            }
        }

        public decimal GetOrderTotal()
        {
            return orderLines.Sum(line => line.GetTotalPrice());
        }
    }
}