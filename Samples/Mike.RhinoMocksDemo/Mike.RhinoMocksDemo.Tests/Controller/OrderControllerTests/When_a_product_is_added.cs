using Mike.RhinoMocksDemo.Controller;
using Mike.RhinoMocksDemo.Model;
using Mike.RhinoMocksDemo.Repository;
using Mike.RhinoMocksDemo.Service;
using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Controller.OrderControllerTests
{
    [TestFixture]
    public class When_a_product_is_added
    {
        private OrderController orderController;
        private const int customerId = 44;
        private const int productId = 13;
        private const int quantity = 1;

        private Customer customer;
        private Product product;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer
            {
                Id = customerId,
                CurrentOrder = new Order()
            };

            product = new Product { Id = productId };

            var customerRepository = MockRepository.GenerateStub<IRepository<Customer>>();
            var productRepository = MockRepository.GenerateStub<IRepository<Product>>();
            var countryRepository = MockRepository.GenerateStub<IRepository<Country>>();
            var userService = MockRepository.GenerateStub<IUserService>();

            customerRepository.Stub(r => r.GetById(customerId)).Return(customer);
            productRepository.Stub(r => r.GetById(productId)).Return(product);

            orderController = new OrderController(customerRepository, productRepository, countryRepository, userService);

            orderController.AddProduct(customerId, productId, quantity);
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