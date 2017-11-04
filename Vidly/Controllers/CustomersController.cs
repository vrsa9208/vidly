using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

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
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);
            if (customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }

        public ActionResult New()
        {
            CustomerFormViewModel Model = new CustomerFormViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", Model);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                Customer customerInDb = _context.Customers.Single(x => x.Id == customer.Id);

                //TryUpdateModel(customerInDb,"", new string[] { "Name", "Birthdate"}); //Not the best approach
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");  
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return HttpNotFound();

            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}