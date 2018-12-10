using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class ToolController : Controller
    {
        private readonly IToolDal _toolDal;


        public ToolController(IToolDal toolDal)
        {
            _toolDal = toolDal;
        }

        public IActionResult Index()
        {//Shows all tools
            return View();
        }

        //public IActionResult getAllTools()
        //{
        //    return View();
        //}

        public IActionResult ToolDetail()
        {
            return View();
        }
    }
}