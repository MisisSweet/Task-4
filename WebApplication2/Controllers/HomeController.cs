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

        private static List<SelectablePerson> records = new List<SelectablePerson>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["loginUser"] = Person.LoginUser;

            List<Person> persons = DBService.Instanse.GetUsers();


            //use AutoMapper
            return View(persons.Select(m => new SelectablePerson { Password = m.Password,ID=m.ID,Information=m.Information, Login = m.Login }).ToList());

            //return View(records);
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
        [HttpPost]
        public IActionResult onTapBlock(List<SelectablePerson> items)
        {
            var userSelectedPerson = items.Where(m => m.IsSelected).ToList();
            if (userSelectedPerson != null && userSelectedPerson.Any())
            {
                updateStatus(1,userSelectedPerson);
                bool isExit = false;
                userSelectedPerson.ForEach(m =>
                {
                    isExit = m.ID == Person.LoginUser.ID;
                });
                if(isExit)
                    return RedirectToAction("onTapExit", "Home");

            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult onTapUNBlock(IList<SelectablePerson> items)
        {
            var userSelectedPerson = items.Where(m => m.IsSelected).ToList();
            if (userSelectedPerson != null && userSelectedPerson.Any())
            {
                updateStatus(0, userSelectedPerson);
            }

            return RedirectToAction("Index", "Home", new { sel = items.Count });
        }
        private void updateStatus(int value,List<SelectablePerson> persons)
        {
            foreach (SelectablePerson person in persons)
            {
                if (person.IsSelected)
                {
                    DBService.updateStatus(person.Information.ID, value);
                }
            }
        }
        public IActionResult onTapDelete(IList<SelectablePerson> model)
        {
            HomeController.records = model as List<SelectablePerson>;

            return RedirectToAction("Index", "Home");
        }
        public IActionResult onTapExit()
        {
            return Redirect("~/LoginPage");
        }
    }

}
