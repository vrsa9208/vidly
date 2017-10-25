using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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
            return View(_context.Customers.Include(c => c.MembershipType).ToList());
        }

        //GET: Customers/Details/{id}
        [Route("Customers/Details/{id:Int}")]
        public ActionResult Details(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }
    }
}