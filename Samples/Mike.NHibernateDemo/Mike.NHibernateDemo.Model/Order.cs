using System;
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
    }
}