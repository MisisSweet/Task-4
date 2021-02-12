using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static bool allSelected = true;

        private static List<Record> records = new List<Record>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            records = new List<Record>(); 
            string strParam = HttpContext.Request.Query["all_select"];
            if (!string.IsNullOrEmpty(strParam))
            {
                if (bool.Parse(strParam))
                {
                    ViewData["btn_text"] = "Снять все";
                    HomeController.allSelected = true;
                }
                else
                {
                    ViewData["btn_text"] = "Выделить все";
                    HomeController.allSelected = false;
                }
                
            }
            else
            {
                ViewData["btn_text"] = "Выделить все";
                allSelected = false;
            }

            List<Person> users = DBService.Instanse.GetUsers();
            ViewData["loginUser"] = Person.LoginUser;

            users.ForEach((user) => { records.Add(new Record(user,allSelected)); });
            ViewData["records"] = records;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult onTapBlock(List<Record> records)
        {
            HomeController.records = records;
            updateStatus(1);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult onTapUNBlock(List<Record> records)
        {
            HomeController.records = records;
            updateStatus(0);
            return RedirectToAction("Index", "Home");
        }
        private void updateStatus(int value)
        {
            foreach (Record record in records)
            {
                if (record.select)
                {
                    DBService.updateStatus(record.user.Information.ID, value);
                }
            }
        }
        public IActionResult onTapDelete()
        {
            return RedirectToAction("Index","Home");
        }
        public IActionResult onTapExit()
        {
            return Redirect("~/LoginPage");
        }
        public IActionResult onChange()
        {
            return RedirectToAction("Index", "Home", new { all_select = !HomeController.allSelected });
        }
    }
}
