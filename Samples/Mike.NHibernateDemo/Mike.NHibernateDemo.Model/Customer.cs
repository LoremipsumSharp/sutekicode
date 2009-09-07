using System;
using System.Collections.Generic;

namespace Mike.NHibernateDemo.Model
{
    public class Customer : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual int Version { get; protected set; }

        private IList<Order> orders = new List<Order>();

        public virtual IList<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }

        public virtual bool HasOrders()
        {
            return orders.Count > 0;
        }

        public virtual Order GetCurrentOrder()
        {
            return !HasOrders() ? null : orders[orders.Count - 1];
        }

        public static Customer NullCustomer
        {
            get
            {
                return new Customer {Name = "Not set"};
            }
        }

        public virtual Order CreateOrder()
        {
            var order = new Order
            {
                OrderDate = DateTime.Now, 
                Customer = this
            };
            orders.Add(order);
            return order;
        }
    }
}