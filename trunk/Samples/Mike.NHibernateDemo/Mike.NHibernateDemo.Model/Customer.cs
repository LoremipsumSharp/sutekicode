using System.Collections.Generic;

namespace Mike.NHibernateDemo.Model
{
    public class Customer
    {
        private IList<Order> orders = new List<Order>();

        public IList<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public string Name { get; set; }

        public static Customer NullCustomer
        {
            get
            {
                return new Customer {Name = "Not set"};
            }
        }
    }
}