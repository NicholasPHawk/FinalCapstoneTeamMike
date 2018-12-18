using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;
using FinalCapstone.Extensions;
using FinalCapstone.Dal;

namespace FinalCapstone.Controllers
{
    public class ParentController : Controller
    {
        /// Returns bool if user has authenticated in       
        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Session.Get<string>("Tool_Geek_UserName") != null;
            }
        }
    }
}
