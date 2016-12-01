using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
           _context.Dispose();
        }

        [HttpPost]        //POST /api/NewRentals/ video number-108
        public IHttpActionResult AddMovies(NewRental newRental)
        {
              var customerInDb = _context.Customers.Single(c => c.Id == newRental.CustomerId);
                //This Single() method gives Wake internal error response if found errors
                //We can also use SingleOrDefault()

                var moviesInDb = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
                //To load multiple movies it's translated equivalent SQL statement will be
                //SELECT *
                //FROM Movies
                //WHERE Id IN(1,2,3);

                foreach (var movie in moviesInDb)
                {
                    if (movie.NumberAvailable == 0)
                        return BadRequest("Movie is not available");

                    movie.NumberAvailable--;

                    var rental = new Rental();
                    rental.customer = customerInDb;
                    rental.movie = movie;
                    rental.DateRented = DateTime.Now;

                    _context.Rentals.Add(rental);

                };
                _context.SaveChanges();
            
            //throw new NotImplementedException();
            return Ok();
        }
    }
}
