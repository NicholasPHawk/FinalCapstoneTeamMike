using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;


namespace FinalCapstone.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly ILibrarianDal _librarianDal;

        public LibrarianController(ILibrarianDal librarianDal)
        {
            _librarianDal = librarianDal;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _librarianDal.GetLibrarian(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("invalid-user", "The username provided does not exist");
                return View("Login", model);
            }
            //else if (user.Password != model.Password)
            //else if (!passwordHelper.ValidateHash(model, user))
            //{
            //    ModelState.AddModelError("invalid-password", "The password provided is not valid");
            //    return View("Login", model);
            //}

            //base.LogLibrarianIn(user.Username);

           // return RedirectToAction("Index", "Home");
            return View();
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
            newUser.Address = model.Address;
            _librarianDal.RegisterUser(newUser);
            return RedirectToAction("Index","Tool");
        }

        [HttpGet]
        public IActionResult RegisterLibrarian()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterLibrarian(NewLibrarianModel model)
        {
            Librarian newLibrarian = new Librarian();
            newLibrarian.Username = model.Username;
            newLibrarian.Password = model.Password;
            newLibrarian.Salt = "?";
            _librarianDal.RegisterLibrarian(newLibrarian);

            return RedirectToAction("Index", "Tool");
        }
    }
}