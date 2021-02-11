using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    public class LoginPageController : Controller
    {
        public IActionResult Index()
        {
            string strParam = HttpContext.Request.Query["error"];
            if (!string.IsNullOrEmpty(strParam))
                ViewData["error"] = "Введен неверный логин или пароль";
            return View();
        }
        
        public IActionResult onTapLogin(string login, string password)
        {
            Person person;
            if (DBService.Instanse.Login(login, password))
            {
                ViewData["Error"] = "";
                return Redirect("~/Home");
            }
            else
            {
                return RedirectToAction("Index", "LoginPage", new { error = true });
            }
        }
       
        public IActionResult onTapRegistration(string reg)
        {
            return Redirect("~/RegisterPage");
        }
    }
}
