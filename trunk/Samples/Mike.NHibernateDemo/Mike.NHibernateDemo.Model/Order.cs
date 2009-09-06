using System;
using System.Linq;
using System.Collections.Generic;

namespace Mike.NHibernateDemo.Model
{
    public class Order : IEntity
    {
        public virtual int Id { get; protected set; }

        private IList<OrderLine> orderLines = new List<OrderLine>();

        public virtual IList<OrderLine> OrderLines
        {
            get { return orderLines; }
            set { orderLines = value; }
        }

        public virtual DateTime OrderDate { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual void AddOrderLine(OrderLine orderLine)
        {
            orderLine.Order = this;
            orderLines.Add(orderLine);
        }

        public virtual void AddProduct(Product product)
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

        public virtual decimal GetOrderTotal()
        {
            return orderLines.Sum(line => line.GetTotalPrice());
        }
    }
}