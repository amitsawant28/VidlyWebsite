using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;


namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Required]
        [Display(Name = "Date Of Release")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 10)]
        public byte? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                if (Id !=0)
                    return "Edit Movie";
                else
                    return "New Movie";
            }
            //Or 
            
            //get{ return (Id!=0)? "Edit Movie":"New Movie"}
        }

        public MovieFormViewModel()//constructor for new movie
        {
            Id = 0;
        }
        public MovieFormViewModel(Movie movie)//constructor for existing movie
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;

        }
    }
}