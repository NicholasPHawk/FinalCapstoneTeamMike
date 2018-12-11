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
            IList<Tool> tools = _toolDal.GetTools(false);
            return View(tools);
        }

        public IActionResult CheckedOutTools()
        {
            IList<Tool> tools = _toolDal.GetTools(true);
            return View(tools);
        }

        public IActionResult ToolDetail(int id)
        {
            bool isCheckedOut = _toolDal.CheckToolAvailability(id);
            Tool tool = _toolDal.GetToolDetails(id, isCheckedOut);
            return View(tool);
        }
    }
}