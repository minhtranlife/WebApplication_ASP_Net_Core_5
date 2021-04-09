using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            string today = DateTime.Now.ToShortDateString();
            ViewData["Title"] = "Login";
            ViewData["Day"] = today;
            return View("Views/Login/Login.cshtml");
            //return Ok(today);
        }
    }
}
