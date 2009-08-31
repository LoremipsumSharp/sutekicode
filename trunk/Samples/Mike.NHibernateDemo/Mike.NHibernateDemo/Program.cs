using System;
using Mike.NHibernateDemo.Model;

namespace Mike.NHibernateDemo
{
    class Program
    {
        static void Main()
        {
            var customer = CreateCustomerWithOrder();
            PrintCustomer(customer);
        }

        private static Customer CreateCustomerWithOrder()
        {
            return new Customer
            {
                Name = "John Brown",
                Orders =
                    {
                        new Order
                        {
                            OrderDate = new DateTime(2009, 8, 23),
                            OrderLines =
                                {
                                    new OrderLine
                                    {
                                        Quantity = 2,
                                        Product = new Product
                                        {
                                            Name = "Widget",
                                            Price = 11.26M
                                        }
                                    }
                                }
                        }
                    }
            };
        }

        private static void PrintCustomer(Customer customer)
        {
            Console.WriteLine("Customer: {0}", customer.Name);
            foreach (var order in customer.Orders)
            {
                Console.WriteLine("\tOrder Date: {0}", order.OrderDate.ToShortDateString());
                foreach (var orderLine in order.OrderLines)
                {
                    Console.WriteLine("\t\tOrderLine: {0} x {1} = {2}", 
                        orderLine.Product.Name, 
                        orderLine.Quantity, 
                        orderLine.GetTotalPrice());
                }
            }
        }
    }
}
