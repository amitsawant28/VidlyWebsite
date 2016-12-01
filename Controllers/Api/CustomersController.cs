using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _Context;

        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }
        
        
        //GET /api/Customers get list of all customers
        public IEnumerable<Customer> GetCustomers(string query = null)
        {
            var customersQuery = _Context.Customers.Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            var customer = customersQuery.ToList();
            return customer;
            
        }

       
        //GET /api/Customers/1 get details of customer having Id=1
        public Customer GetCustomer(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return customer;
        }

        [AllowAnonymous]
        //POST /api/Customers Creating a new customer
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid) //Client side validation
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            else
            {
                _Context.Customers.Add(customer);
                _Context.SaveChanges();
            }
            return customer;
        }

        //PUT /api/Customers/1 Updating existing customer data
        [HttpPut]
        public Customer UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid) //Client side validation
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _Context.SaveChanges();

            return customer;
        }

        [Authorize(Roles = RollName.Admin)]
        //DELETE /api/Customers/1 deleting customer having Id=1 Modified
        [HttpDelete]
        public IEnumerable<Customer> DeleteCustomer(int id)
        {
            var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                _Context.Customers.Remove(customerInDb);
                _Context.SaveChanges();
            }

            return _Context.Customers.ToList();
        }
        //public void DeleteCustomer(int id)
        //{
        //    var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == id);
        //    if (customerInDb == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    else
        //        _Context.Customers.Remove(customerInDb);
        //        _Context.SaveChanges();

        //}

    }
}
