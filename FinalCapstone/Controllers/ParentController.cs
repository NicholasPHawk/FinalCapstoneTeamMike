using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Extensions;

namespace FinalCapstone.Controllers
{
    public class ParentController : Controller
    {
        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Session.Get<string>("Tool_Geek_UserName") != null;
            }
        }
    }
}
