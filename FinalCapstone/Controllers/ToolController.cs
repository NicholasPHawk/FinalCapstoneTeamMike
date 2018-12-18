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

        [HttpGet]
        public IActionResult ToolLoanRecordSearch()
        {
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
            IList<Tool> removeToolList = new List<Tool>();
            removeToolList = _toolDal.RemoveAToolList();
            return View(removeToolList);
        }

        [HttpPost]
        public IActionResult RemoveATool(Tool tool)
        {
            _toolDal.RemoveATool(tool);
            return RedirectToAction("RemoveATool");
        }

        [HttpGet]
        public IActionResult AddTool()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTool(Tool tool)
        {
            _toolDal.AddTool(tool);
            return RedirectToAction("AddTool");
        }
    }
}