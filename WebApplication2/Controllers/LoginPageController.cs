using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class LoginPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult onTapLogin(string login)
        {
            return Redirect("~/RegisterPage");
        }
        public IActionResult onTapRegistration(string reg)
        {
            return Redirect("~/RegisterPage");
        }
    }
}
