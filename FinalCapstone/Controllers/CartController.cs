using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using FinalCapstone.Models;

namespace FinalCapstone.Controllers
{
    public class CartController : ParentController
    {
        private readonly IToolDal _toolDal;

        public CartController(IToolDal toolDal)
        {
            _toolDal = toolDal;
        }

        public IActionResult Index()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Tool> tools = _toolDal.GetTools(false);
            IList<User> users = _toolDal.GetUsers();

            CartViewModel model = new CartViewModel
            {
                Tools = tools,
                Users = users
            };

            List<SelectListItem> borrowers = new List<SelectListItem>();

            foreach (User user in users)
            {
                SelectListItem name = new SelectListItem() { Text = user.Name, Value = user.Name };
                borrowers.Add(name);
            }

            model.Borrowers = borrowers;
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(CartViewModel model)
        {
            Tool tool = _toolDal.GetToolDetails(model.Id, false);
            tool.CheckedOut = true;
            tool.CurrentBorrowerName = model.Borrower;
            tool.DateBorrowed = DateTime.Now;
            tool.DueDate = DateTime.Now.AddDays(model.Days);

            _toolDal.ChangeCheckedOutStatus(tool.Id, true);

            Cart cart = GetActiveCart();
            cart.AddToCart(tool);

            SaveActiveCart(cart);

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            _toolDal.ChangeCheckedOutStatus(id, false);

            Cart cart = GetActiveCart();
            cart.RemoveFromCart(id);

            SaveActiveCart(cart);

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            Cart cart = GetActiveCart();
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            Cart cart = GetActiveCart();
            _toolDal.CheckOut(cart);
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

        private Cart GetActiveCart()
        {
            Cart cart = null;

            if (HttpContext.Session.Get<Cart>("Cart") == null)
            {
                cart = new Cart();
                SaveActiveCart(cart);
            }
            else
            {
                cart = HttpContext.Session.Get<Cart>("Cart");
            }

            return cart;
        }

        private void SaveActiveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }
    }
}