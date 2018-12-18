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
        private const string ConfirmationKey = nameof(ConfirmationKey);

        private readonly IToolDal _toolDal;

        public ToolController(IToolDal toolDal)
        {
            _toolDal = toolDal;
        }

        public IActionResult Index()
        {
            IList<Tool> tools = _toolDal.GetTools(false);

            AvailableToolsViewModel model = new AvailableToolsViewModel
            {
                Tools = tools,
                SuccessMessage = TempData[ConfirmationKey] as string
            };

            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(model);
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

            IList<Tool> tools = _toolDal.RemoveAToolList();

            RemoveAToolViewModel model = new RemoveAToolViewModel
            {
                Tools = tools,
                SuccessMessage = TempData[ConfirmationKey] as string
            };

            ViewBag.IsLoggedIn = IsAuthenticated;
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveATool(Tool tool)
        {
            _toolDal.RemoveATool(tool);
            TempData[ConfirmationKey] = "Tool was successfully removed.";

            return RedirectToAction("RemoveATool");
        }

        [HttpGet]
        public IActionResult AddTool()
        {
            AddToolViewModel model = new AddToolViewModel
            {
                SuccessMessage = TempData[ConfirmationKey] as string
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddTool(AddToolViewModel model)
        {
            Tool tool = new Tool
            {
                Brand = model.Brand,
                ToolName = model.ToolName,
                Description = model.Description
            };

            _toolDal.AddTool(tool);
            TempData[ConfirmationKey] = "Tool was successfully added.";

            return RedirectToAction("AddTool");
        }
    }
}