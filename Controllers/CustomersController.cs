using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _Context;
        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
           _Context.Dispose();
        }


        public ActionResult Create()// vid35
            //This Create action/method when called in URL,
            //it redirects the user to new CustomerForm Page 
        {
            var membershipTypes = _Context.MembershipTypes.ToList();

            //ViewModel is used to pass multiple objects to View()
            var viewmodel = new CustomerFormViewModel
            {
                Customer = new Customer(),//to set Customer Id & other properties from null to 0
                //and to remove null Id field error in validation summary form page. 
                MembershipTypes=membershipTypes
            };

            if (User.IsInRole(RollName.Admin))
                return View("CustomerForm","~/Views/Shared/AdminHomePage.cshtml", viewmodel);
            else
                return View("CustomerForm", "~/Views/Shared/_Layout.cshtml", viewmodel);
                
        }

        [HttpPost] //this action/method should accept form data only through POST method not through others
        [ValidateAntiForgeryToken]        
        public ActionResult Save(Customer customer)//it accpets parameter of customer type
        {
            //Here I renamed create action to save action for both creating and updating form data
            if(!ModelState.IsValid)
            {
                var viewmodel= new CustomerFormViewModel
                {
                    Customer=customer,
                    MembershipTypes=_Context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewmodel);
            }
            
            if(customer.Id == 0)
            {
                _Context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _Context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _Context.SaveChanges();
            if (User.IsInRole(RollName.Admin))
                return RedirectToAction("Index", "Customers");
            else
                return RedirectToAction("CustomerListOnly", "Customers");
            
            //DBCC CHECKIDENT ('[Customers]',RESEED,3); Use this command to reset auto_increment problem of identity in SQL server
        }


        public ActionResult Index()
         {
             if (User.IsInRole(RollName.Admin))
                 return View("Index", "~/Views/Shared/AdminHomePage.cshtml");
             else
                 return View("CustomerListOnly","~/Views/Shared/_Layout.cshtml");
             //var customers = _Context.Customers.Include(c => c.MembershipType);//silly mistake I made
            // return View(customers.ToList());
             
             //return View(customers.AsEnumerable());
         }

        public ActionResult CustomerListOnly()
        {
            return View();
        }
        
        public ActionResult Edit(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var RentedMovies = _Context.Rentals.Include(c=>c.customer).Include(r=>r.movie);
            
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _Context.MembershipTypes.ToList(),
               //An important query for getting combined records
                MoviesList = RentedMovies.Where(m=>m.customer.Id == id)
            };
            

            //var JoinQuery = _Context.Rentals
            //                .Include(c=>c.customer)
            //                .Include(m=>m.movie);

                //JoinQuery = (from r in _Context.Rentals
                //             join c in _Context.Customers on r.customer.Id equals c.Id
                //             join m in _Context.Movies on r.movie.Id equals m.Id
                //             orderby r.customer.Id
                //             select new CustomerFormViewModel
                //             {
                //                 Customer = customer,
                //                 MembershipTypes = _Context.MembershipTypes.ToList(),
                //                 MoviesList = _Context.Rentals.Include(m => m.movie.Id).Contains(m.Id) //_context.rentals.select(s=>joinquery.contains(m.id)).tostring().tolist() //movie.contains(s.movie.id)).tolist()

                //             });

            return View("CustomerEditForm",viewModel);
        }

         
         public ActionResult Details(int id)
         {
             var customer = _Context.Customers.
                 Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
             if (customer == null)
                 return HttpNotFound();
           /*  else
             {
                 var viewModel = new VidlyViewModel
                 {
                     Customers = customer,
                     MembershipTypes = _Context.MembershipTypes.ToList();
                    var movieId = _Context.Rentals
                 }
             }
             return View(customer); */
             return View(customer);

         }

         //public IEnumerable<Customer> GetCustomers()
         //{
         //    return new List<Customer>
         //    {
         //        new Customer { Id = 1, Name = "John Smith" },
         //        new Customer { Id = 2, Name = "Mary Williams" }
         //    };
         //}
     }
 } 