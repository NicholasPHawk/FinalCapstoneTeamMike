using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;


namespace FinalCapstone.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDal _userDal;

        public UserController(IUserDal userDal)
        {
            _userDal = userDal;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(NewUserViewModel model)
        {
            User newUser = new User();
            newUser.Email = model.Email;
            newUser.Name = model.Name;
            newUser.DriversLicense = model.DriversLicense;
            newUser.Password = model.Password;
            newUser.Username = model.Email;
            newUser.Email = model.Email;
            newUser.Salt = "?";
            _userDal.RegisterUser(newUser);
            return RedirectToAction();
        }

    }
}