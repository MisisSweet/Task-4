using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    public class RegisterPageController : Controller
    {
        public IActionResult Index()
        { 

            return View();
        }
        public IActionResult onTapRegistr(string lastname, string firstname, string email, string login, string password)
        {
            DBService.Instanse.AddUser(lastname, firstname, email, login, password);
            return Redirect("~/LoginPage");
        }
    }
}
