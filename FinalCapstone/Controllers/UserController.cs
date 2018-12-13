using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Mvc;


namespace FinalCapstone.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDAL _userDal;

        public UserController(IUserDAL userDal)
        {
            _userDal = userDal;
        }

    }
}