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
    }
}
