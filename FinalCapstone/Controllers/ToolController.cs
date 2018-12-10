using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;

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
            IList<Tool> tools = _toolDal.GetTools();
            return View(tools);
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