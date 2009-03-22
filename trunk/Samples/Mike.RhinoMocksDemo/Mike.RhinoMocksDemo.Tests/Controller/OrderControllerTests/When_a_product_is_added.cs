using Mike.RhinoMocksDemo.Controller;
using Mike.RhinoMocksDemo.Model;
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
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer
            {
                Id = customerId,
                CurrentOrder = new Order()
            };

            orderController = new OrderController();

            orderController.AddProduct(customerId, productId);
        }
    }
}