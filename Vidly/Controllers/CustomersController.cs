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

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(GetCustomers());
        }

        //GET: Customers/Details/{id}
        [Route("Customers/Details/{id:Int}")]
        public ActionResult Details(int id)
        {
            Customer customer = GetById(id);
            if (customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }

        private Customer GetById(int id)
        {
            //var query = from customer in GetCustomers()
            //            where customer.Id == id
            //            select customer;

            //return query.FirstOrDefault();
            return GetCustomers().SingleOrDefault(x => x.Id == id);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            /*return new List<Customer>
            {
                new Customer{ Name = "John Smith", Id = 1 },
                new Customer{ Name = "Mary William", Id = 2 }
            };*/
            return _context.Customers;
        }
    }
}