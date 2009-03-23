using Mike.RhinoMocksDemo.Model;
using Mike.RhinoMocksDemo.Repository;
using Mike.RhinoMocksDemo.Service;

namespace Mike.RhinoMocksDemo.Controller
{
    public class OrderController
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Product> productRepository;

        // let's just pretend we need these too
        private readonly IRepository<Country> countryRepository;
        private readonly IUserService userService;

        public OrderController(
            IRepository<Customer> customerRepository, 
            IRepository<Product> productRepository, 
            IRepository<Country> countryRepository, 
            IUserService userService)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.countryRepository = countryRepository;
            this.userService = userService;
        }

        public void AddProduct(int customerId, int productId, int quantity)
        {
            var customer = customerRepository.GetById(customerId);
            var product = productRepository.GetById(productId);

            customer.CurrentOrder.AddOrderLine(quantity, product);
        }

        // lots of other stuff here
    }
}