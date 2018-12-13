using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;


namespace FinalCapstone.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }
    }
}