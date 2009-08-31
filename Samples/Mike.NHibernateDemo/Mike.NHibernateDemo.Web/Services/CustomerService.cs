using System.Web;
using Mike.NHibernateDemo.Model;

namespace Mike.NHibernateDemo.Web.Services
{
    public class CustomerService
    {
        public Customer GetCurrentCustomer()
        {
            return HttpContext.Current.Session["currentCustomer"] as Customer ?? Customer.NullCustomer;
        }

        public void SetCurrentCustomer(Customer customer)
        {
            HttpContext.Current.Session["currentCustomer"] = customer;
        }
    }
}