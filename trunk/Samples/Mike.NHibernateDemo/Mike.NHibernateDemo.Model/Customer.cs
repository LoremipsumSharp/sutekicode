using System.Collections.Generic;

namespace Mike.NHibernateDemo.Model
{
    public class Customer
    {
        public virtual int Id { get; set; }

        private IList<Order> orders = new List<Order>();

        public virtual IList<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }

        public virtual void AddOrder(Order order)
        {
            order.Customer = this;
            orders.Add(order);
        }

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
    }
}