using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    
    public class MoviesController : Controller
    {
        private ApplicationDbContext _Context;
        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
           _Context.Dispose();
        }

        [Authorize(Roles = RollName.Admin)] //for limiting anonymous access
        public ActionResult New()   //It takes genre type info from DB
            //This New action/method when called in URL,
            //it redirects the user to new MovieForm Page 
        {
            var genres = _Context.Genres.ToList();
            var viewmodel = new MovieFormViewModel
            {
                //Movie = new Movie(), instead of this we pass seperate properties through  view model
                Genres = genres
            };
            return View("MovieForm",viewmodel);
        }

        // GET: Movies
        public ActionResult Index() //to be fetched from API using jquery and data table
        {
            //var movies = _Context.Movies.Include(g => g.Genre);
            //return View(movies);
            if (User.IsInRole(RollName.Admin))
                return View("List");
            else
                return View("ReadOnlyList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RollName.Admin)]
        public ActionResult Save(Movie movie) //vid 39
        {
            if(!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel(movie)
                {
                    Genres = _Context.Genres.ToList()
                };
                return View("MovieForm",viewmodel);
            }

            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _Context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _Context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }            
            _Context.SaveChanges();
            
            return RedirectToAction("List","Movies");
        }

        [Authorize(Roles = RollName.Admin)] //for limiting anonymous access
        public ActionResult Edit(int id) //vid 40
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewmodel = new MovieFormViewModel(movie)
            {
                Genres = _Context.Genres.ToList()
            };

            return View("MovieForm", viewmodel);
        }

        [Authorize(Roles = RollName.Admin)]
        public ActionResult Details(int id)
        {
            var movie = _Context.Movies.
                Include(g => g.Genre).SingleOrDefault(g => g.Id == id);

            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        //Custom Routing Example below
        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]

        //[Route("movies/released/{year:regex(\\d{4}):range(1,1999)}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByRelaseDate(int year,int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}   