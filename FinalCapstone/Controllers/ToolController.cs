using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;

namespace FinalCapstone.Controllers
{
    public class ToolController : ParentController
    {
        private readonly IToolDal _toolDal;

        public ToolController(IToolDal toolDal)
        {
            _toolDal = toolDal;
        }

        public IActionResult Index()
        {
            IList<Tool> tools = _toolDal.GetTools(false);
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(tools);
        }

        public IActionResult CheckedOutTools()
        {
            IList<Tool> tools = _toolDal.GetTools(true);
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(tools);
        }

        public IActionResult ToolDetail(int id)
        {
            bool isCheckedOut = _toolDal.CheckToolAvailability(id);
            Tool tool = _toolDal.GetToolDetails(id, isCheckedOut);
            return View(tool);
        }

        [HttpGet]
        public IActionResult ToolLoanRecordSearch()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View();
        }

        [HttpPost]
        public IActionResult ToolLoanRecordSearch(ToolLoanRecordSearchModel model)
        {
            IList<ToolLoanRecordSearchModel> tools = new List<ToolLoanRecordSearchModel>();
            tools = _toolDal.GetLoanRecords(model.SearchType, model.SearchString);
            return View("ToolLoanRecordSearchResult", tools);
        }

        [HttpGet]
        public IActionResult RemoveATool()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<Tool> removeToolList = new List<Tool>();
            removeToolList = _toolDal.RemoveAToolList();
            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(removeToolList);
        }

        [HttpPost]
        public IActionResult RemoveATool(Tool tool)
        {
            _toolDal.RemoveATool(tool);
            return RedirectToAction("RemoveATool");
        }
    }
}