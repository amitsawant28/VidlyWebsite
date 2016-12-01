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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _Context;
        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }

        
        public IEnumerable<Movie> GetMovies(string query = null)
        {
            var moviesQuery = _Context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery.ToList();
                
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok();
        }


       /* my old code in working condition also
        //GET /api/Movies get list of all movies
        public IEnumerable<Movie> GetMovies(string query)
        {
            var movies = _Context.Movies.Include(m => m.Genre);
             return movies.Where(m => m.Name.Contains(query)).ToList();  //used include to add other model data in API
        }


        //GET /api/Movies/1 get details of Movie having Id=1
        public Movie GetMovie(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return movie;            
        }
        */ 

        //POST /api/Movies Creating a new movie
        [HttpPost]
        [Authorize(Roles = RollName.Admin)]
        public Movie CreateMovie(Movie movie)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            else
            {
                _Context.Movies.Add(movie);
                _Context.SaveChanges();
            }
            return movie;
        }

         //PUT /api/Movies/1 Updating existing movie data
        [HttpPut]
        [Authorize(Roles = RollName.Admin)]
        public Movie UpdateMovie(int id,Movie movie)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
                var movieInDb = _Context.Movies.SingleOrDefault(m => m.Id == id);
                if(movieInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                else
                {
                    movieInDb.Name = movie.Name;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                    movieInDb.NumberInStock = movie.NumberInStock;
                }

                _Context.SaveChanges();
                return movie;
        }

        //DELETE /api/Movies/1 deleting movie having Id=1 Modified
        [HttpDelete]
        [Authorize(Roles = RollName.Admin)]
        public IEnumerable<Movie> DeleteMovie(int id)
        {
            var movieInDb = _Context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                _Context.Movies.Remove(movieInDb);
                _Context.SaveChanges();
            }
            return _Context.Movies.ToList();
        }

    }
}
