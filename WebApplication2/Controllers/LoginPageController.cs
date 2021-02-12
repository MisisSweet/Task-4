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
            string strParam1 = HttpContext.Request.Query["block"];
            if (!string.IsNullOrEmpty(strParam1))
                ViewData["error"] = "Ваш аккаунт заблокирован";
            return View();
        }
        
        public IActionResult onTapLogin(string login, string password)
        {
            if (DBService.Instanse.Login(login, password))
            {
                if (Person.LoginUser.Information.Status == 1)
                {
                    return RedirectToAction("Index", "LoginPage", new { block = true });
                }
                //ViewData["error"] = "";
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
