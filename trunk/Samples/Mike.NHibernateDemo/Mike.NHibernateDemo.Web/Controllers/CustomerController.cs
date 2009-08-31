using System;
using System.Web.Mvc;
using Mike.NHibernateDemo.Model;
using Mike.NHibernateDemo.Web.Services;

namespace Mike.NHibernateDemo.Web.Controllers
{
    [HandleError]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;

        public CustomerController()
        {
            customerService = new CustomerService();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            var customer = customerService.GetCurrentCustomer();

            return View(customer);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            var customer = new Customer
            {
                Name = "Mike",
                Orders =
                    {
                        new Order
                        {
                            OrderDate = DateTime.Now
                        }
                    }
            };

            customerService.SetCurrentCustomer(customer);

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddWidget()
        {
            var customer = customerService.GetCurrentCustomer();

            if(customer.HasOrders())
            {
                customer.GetCurrentOrder().AddProduct(new Product { Name = "Widget", Price = 3.45M });
            }

            return RedirectToAction("Index");
        }

    }
}
