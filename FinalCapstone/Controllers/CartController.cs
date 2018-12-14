using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalCapstone.Dal;
//using FinalCapstone.Extensions;
using FinalCapstone.Models;
using FinalCapstone.Models.Cart;

namespace FinalCapstone.Controllers
{
    public class CartController : Controller
    {
        private readonly IToolDal _toolDal;

        public CartController(IToolDal toolDal)
        {
            _toolDal = toolDal;
        }

        public IActionResult Index()
        {
            IList<Tool> tools = _toolDal.GetTools(false);
            IList<User> users = _toolDal.GetUsers();

            AddToCartModel model = new AddToCartModel
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(AddToCartModel addModel)
        {
            Tool tool = _toolDal.GetToolDetails(addModel.Id, false);
            tool.CheckedOut = true;
            tool.CurrentBorrowerName = addModel.Borrower;
            tool.DateBorrowed = DateTime.Now;
            tool.DueDate = DateTime.Now.AddDays(addModel.Days);

            //Cart cart = GetActiveCart();

            ViewCartModel viewModel = new ViewCartModel();
            viewModel.Cart.AddToCart(tool);

            return RedirectToAction(nameof(ViewCart), viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult RemoveFromCart(int id)
        //{
        //    Cart cart = GetActiveCart();
        //    cart.Items.RemoveAll(i => i.Tool.Id == id);

        //    return RedirectToAction("ViewCart");
        //}

        public IActionResult ViewCart(ViewCartModel model)
        {
            //Cart cart = GetActiveCart();
            return View(model);
        }

        //private Cart GetActiveCart()
        //{
        //    Cart cart = null;

        //    if (HttpContext.Session.Get<Cart>("Cart") == null)
        //    {
        //        cart = new Cart();
        //        SaveActiveCart(cart);
        //    }
        //    else
        //    {
        //        cart = HttpContext.Session.Get<Cart>("Cart");
        //    }

        //    return cart;
        //}

        //private void SaveActiveCart(Cart cart)
        //{
        //    HttpContext.Session.Set("Cart", cart);
        //}
    }
}