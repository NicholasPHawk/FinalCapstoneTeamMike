using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Helper;
using System.Text.RegularExpressions;
using FinalCapstone.Extensions;

namespace FinalCapstone.Controllers
{
    public class LibrarianController : ParentController
    {
        private const string ConfirmationKey = nameof(ConfirmationKey);

        private readonly ILibrarianDal _librarianDal;
        PasswordHelper passwordHelper = new PasswordHelper();

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
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            Librarian user = _librarianDal.GetLibrarian(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("invalid-user", "Invalid username or password");
                return View("Login");
            }

            else if(!passwordHelper.ValidateHash(model, user))
            {
                ModelState.AddModelError("invalid-password", "Invalid username or password");
                return View("Login");
            }

            LogLibrarianIn(user.Username);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            LogLibrarianOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsLoggedIn = IsAuthenticated;
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
            TempData[ConfirmationKey] = "User was successfully registered.";

            return RedirectToAction("Index","Tool");
        }

        [HttpGet]
        public IActionResult RegisterLibrarian()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View();
        }

        [HttpPost]
        public IActionResult RegisterLibrarian(NewLibrarianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterLibrarian", model);
            }

            Librarian newLibrarian = new Librarian();
            newLibrarian.Username = model.Username;
            newLibrarian.Salt = passwordHelper.CreateSalt();
            newLibrarian.Password = passwordHelper.GenerateSHA256Hash(model.Password, newLibrarian);
            _librarianDal.RegisterLibrarian(newLibrarian);
            TempData[ConfirmationKey] = "Librarian was successfully registered.";

            return RedirectToAction("Index", "Tool");
        }

        private const string UsernameKey = "Tool_Geek_UserName";

        /// Gets the value from the Session
        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user session exists, if not create it
                if (HttpContext.Session.Get<string>("Tool_Geek_UserName") != null)
                {
                    username = HttpContext.Session.Get<string>("Tool_Geek_UserName");
                }

                return username;
            }
        }

        /// "Logs" the current user in
        public void LogLibrarianIn(string username)
        {
            HttpContext.Session.Set("Tool_Geek_UserName", username);
        }

        /// "Logs out" a user by removing the cookie.
        public void LogLibrarianOut()
        {
            HttpContext.Session.Remove("Tool_Geek_UserName");
        }

        //public bool GetAuthenticatedUser()
        //{
        //    Librarian librarian = null;

        //    if (IsAuthenticated)
        //    {
        //        librarian = _librarianDal.GetLibrarian(CurrentUser);
        //    }

        //    return View();
        //}
    }
}