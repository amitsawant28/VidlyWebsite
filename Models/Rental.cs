using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; } //For database Primary kry purpose

        [Required]
        public Customer customer { get; set; }

        [Required]
        public Movie movie { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
        
    }
}