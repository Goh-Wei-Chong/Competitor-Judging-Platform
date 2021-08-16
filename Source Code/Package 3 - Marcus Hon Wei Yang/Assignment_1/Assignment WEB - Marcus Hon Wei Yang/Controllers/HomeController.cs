using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            string loginID = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();

            if (loginID == "judge@cjp.sg" && password == "111") //admin staff user credentials
            {
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "Admin Judge");

                return RedirectToAction("JudgeMain");
                
            }
            else
            {
                TempData["Message"] = "Invalid Login Credentials!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult JudgeMain()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
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
    }
}
