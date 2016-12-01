using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        public ActionResult NewRental()
        {
            if (User.IsInRole(RollName.Admin))
                return View("NewRental", "~/Views/Shared/AdminHomePage.cshtml");
            else
                return View("NewRental", "~/Views/Shared/_Layout.cshtml");
            
        }
    }
}