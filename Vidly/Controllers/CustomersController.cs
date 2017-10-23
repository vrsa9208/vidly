using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer{ Name = "John Smith", Id = 1 },
            new Customer{ Name = "Mary William", Id = 2 }
        };

        // GET: Customers
        public ActionResult Index()
        {
            return View(customers);
        }

        //GET: Customers/Details/{id}
        [Route("Customers/Details/{id:Int}")]
        public ActionResult Details(int id)
        {
            Customer customer = getById(id);
            if (customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }

        private Customer getById(int id)
        {
            var query = from customer in customers
                where customer.Id == id
                select customer;

            return query.FirstOrDefault();
        }
    }
}