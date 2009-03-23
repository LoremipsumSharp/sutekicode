using System;
using Mike.RhinoMocksDemo.Controller;
using Mike.RhinoMocksDemo.Model;
using Mike.RhinoMocksDemo.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Controller.OrderControllerTests
{
    [TestFixture]
    public class When_a_product_is_added_AUTOMOCK
    {
        private OrderController orderController;
        private const int customerId = 44;
        private const int productId = 13;
        private const int quantity = 1;

        private Customer customer;
        private Product product;

        private IDisposable playback;

        [SetUp]
        public void SetUp()
        {
            var mocks = new MockRepository();
            var container = new Rhino.Testing.AutoMocking.AutoMockingContainer(mocks);
            container.Initialize();

            customer = new Customer
            {
                Id = customerId,
                CurrentOrder = new Order()
            };

            product = new Product {Id = productId};

            orderController = container.Create<OrderController>();

            using (mocks.Record())
            {
                    container.Resolve<IRepository<Customer>>().Stub(r => r.GetById(customerId)).Return(customer);
                    container.Resolve<IRepository<Product>>().Stub(r => r.GetById(productId)).Return(product);
            }

            playback = mocks.Playback();

            orderController.AddProduct(customerId, productId, quantity);
        }

        [TearDown]
        public void TearDown()
        {
            playback.Dispose();
        }

        [Test]
        public void The_product_should_be_added_to_the_customers_current_order()
        {
            var theOnlyProduct = customer.CurrentOrder.OrderLines[0].Product;

            Assert.That(theOnlyProduct, Is.SameAs(product));
        }

        [Test]
        public void The_quantity_of_the_orderLine_should_be_correct()
        {
            var theOnlyQuantity = customer.CurrentOrder.OrderLines[0].Quantity;

            Assert.That(theOnlyQuantity, Is.EqualTo(quantity));
        }
    }
}