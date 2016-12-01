using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class VidlyViewModel
    {
        public Movie Movies { get; set; }
        public Customer Customers { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}