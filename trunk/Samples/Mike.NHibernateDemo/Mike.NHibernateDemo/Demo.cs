using System;
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Mike.NHibernateDemo.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq;
using NUnit.Framework;
using Order=Mike.NHibernateDemo.Model.Order;

namespace Mike.NHibernateDemo
{
    [TestFixture]
    public class SchemaCreationDemo
    {
        static SchemaCreationDemo()
        {
            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
        }

        [Test]
        public void Create_the_schema()
        {
            Fluently
                .Configure()
                .Database(
                    MsSqlConfiguration.MsSql2005
                        .ConnectionString(config => config.Server(@"localhost\SQLEXPRESS").Database("NHibernateDemo").TrustedConnection()))
                .Mappings(m => m.AutoMappings.Add(
                    AutoMap
                        .AssemblyOf<Customer>()
                        .Setup(setup => setup.IsComponentType = type => type == typeof(Address))
                        .Override<Customer>(map => map.HasMany(x => x.Orders).Cascade.All())
                        .Override<Order>(map => map.HasMany(x => x.OrderLines).Cascade.All())
                        ))
                .ExposeConfiguration(config => new SchemaExport(config).Create(false, true))
                .BuildSessionFactory();
        }
    }

    [TestFixture]
    public class Demo
    {
        private ISessionFactory sessionFactory;
        private const int customerId = 1;
        private const int widgetId = 1;
        private const int gadgetId = 2;

        [SetUp]
        public void SetUp()
        {
            sessionFactory = Fluently
                .Configure()
                .Database(
                    MsSqlConfiguration.MsSql2005
                        .ConnectionString(config => config.Server(@"localhost\SQLEXPRESS").Database("NHibernateDemo").TrustedConnection()))
                .Mappings(m => m.AutoMappings.Add(
                    AutoMap
                        .AssemblyOf<Customer>()
                        .Setup(setup => setup.IsComponentType = type => type == typeof(Address))
                        .Override<Customer>(map => map.HasMany(x => x.Orders).Cascade.All())
                        .Override<Order>(map => map.HasMany(x => x.OrderLines).Cascade.All())
                        ))
                .BuildSessionFactory();
        }

        [Test]
        public void Insert_a_new_customer()
        {
            using(var session = sessionFactory.OpenSession())
            using(var transaction = session.BeginTransaction())
            {
                var customer = new Customer
                {
                    Name = "Percy",
                    Address = new Address
                    {
                        Line1 = "54 Steam Lane",
                        Town = "Sodor"
                    }
                };

                session.SaveOrUpdate(customer);
                Console.WriteLine("Saved customer with Id = {0}", customer.Id);

                transaction.Commit();
            }
        }

        [Test]
        public void Retrieve_a_customer()
        {
            using(var session = sessionFactory.OpenSession())
            using(var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerId);
                Console.WriteLine("Loaded Customer with Id = {0}", customer.Id);

                transaction.Commit();
            }
        }

        [Test]
        public void Retrieve_a_customers_name()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerId);
                PrintCustomer(customer);

                transaction.Commit();
            }
        }

        [Test]
        public void Loading_the_same_customer_twice_does_not_create_two_instances()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer1 = session.Load<Customer>(customerId);
                PrintCustomer(customer1);

                var customer2 = session.Load<Customer>(customerId);
                PrintCustomer(customer2);
                
                Assert.AreSame(customer1, customer2);

                transaction.Commit();
            }
        }

        [Test]
        public void Change_a_customers_name()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerId);
                customer.Name = "Percy";
                transaction.Commit();
            }
        }

        // create a new customer before running this
        [Test]
        public void Delete_a_customer()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerId + 1);
                session.Delete(customer);
                transaction.Commit();
            }
        }

        // set the name back to Percy first
        [Test]
        public void Query_a_customer_by_name_using_HQL()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session
                    .CreateQuery("from Customer as c where c.Name = ?")
                    .SetParameter(0, "Percy")
                    .UniqueResult<Customer>();
                
                PrintCustomer(customer);
                transaction.Commit();
            }
        }

        [Test]
        public void Change_a_customers_name_using_HQL()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session
                    .CreateQuery("update Customer c set c.Name = ? where c.Name = ?")
                    .SetParameter(0, "Frankie")
                    .SetParameter(1, "Percy")
                    .ExecuteUpdate();

                transaction.Commit();
            }
        }

        [Test]
        public void Query_a_customer_by_name_using_criteria()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session
                    .CreateCriteria(typeof (Customer))
                    .Add(Restrictions.Eq("Name", "Percy"))
                    .UniqueResult<Customer>();

                PrintCustomer(customer);
                transaction.Commit();
            }
        }

        [Test]
        public void Query_a_customer_by_name_using_linq()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session
                    .Linq<Customer>()
                    .SingleOrDefault(c => c.Name == "Percy");

                PrintCustomer(customer);
                transaction.Commit();
            }
        }

        [Test]
        public void Query_a_customer_by_name_using_SQL()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session
                    .CreateSQLQuery("SELECT {c.*} FROM Customer AS c WHERE c.Name = :name")
                    .AddEntity("c", typeof (Customer))
                    .SetParameter("name", "Percy")
                    .UniqueResult<Customer>();

                PrintCustomer(customer);
                transaction.Commit();
            }
        }

        [Test]
        public void Create_some_products()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var widget = new Product { Name = "Widget", Price = 11.26M };
                var gadget = new Product { Name = "Gadget", Price = 5.60M };

                session.SaveOrUpdate(widget);
                session.SaveOrUpdate(gadget);

                transaction.Commit();

                Console.WriteLine("Created Product {0}, with Id {1}", widget.Name, widget.Id);
                Console.WriteLine("Created Product {0}, with Id {1}", gadget.Name, gadget.Id);
            }
        }

        [Test]
        public void Insert_a_new_customer_with_order()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = new Customer
                {
                    Name = "Gordon",
                    Address = new Address
                    {
                        Line1 = "9 Piston Avenue",
                        Town = "Sodor"
                    }
                };

                var order1 = new Order
                {
                    OrderDate = DateTime.Now + TimeSpan.FromDays(-1)
                };
                customer.AddOrder(order1);
                order1.AddProduct(session.Load<Product>(widgetId));
                order1.AddProduct(session.Load<Product>(gadgetId));

                var order2 = new Order
                {
                    OrderDate = DateTime.Now
                };
                customer.AddOrder(order2);
                order2.AddProduct(session.Load<Product>(widgetId));

                session.SaveOrUpdate(customer);
                transaction.Commit();

                Console.WriteLine("Created new customer with Id: {0}", customer.Id);
            }
        }

        // make sure we use the correct id :)
        private const int customerWithOrderId = 5;

        [Test]
        public void Retrieve_customer_with_order()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerWithOrderId);
                PrintCustomer(customer);

                transaction.Commit();
            }
        }

        [Test]
        public void Update_order_quantity()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(customerWithOrderId);

                customer.GetCurrentOrder().OrderLines[0].Quantity = 3;

                transaction.Commit();
            }
        }

        private static void PrintCustomer(Customer customer)
        {
            Console.WriteLine("Customer with Id: {0}, Name: {1}", customer.Id, customer.Name);
            Console.WriteLine("Address: {0}, {1}", customer.Address.Line1, customer.Address.Town);
            foreach (var order in customer.Orders)
            {
                Console.WriteLine("\tOrder Date:  {0}", order.OrderDate.ToShortDateString());
                Console.WriteLine("\tOrder Total: {0}", order.GetOrderTotal());
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