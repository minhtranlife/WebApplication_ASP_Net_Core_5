using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Data;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("login")]
        public IActionResult Login()
        {            
            return View("Views/Login/Login.cshtml");
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult Signin(string username, string password)
        {
            if (username != null && password != null)
            {                
                var userInfo = new Users() {Username = username};

                if (userInfo != null)
                {
                    if (userInfo.Password == password)
                    {
                        HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(userInfo));
                        //return Redirect("/");
                    }
                    else
                    {
                        ViewBag.error = "Invalid Account";
                        return View("Views/Login/Login.cshtml");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Views/Login/Login.cshtml");
                }

            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Views/Login/Login.cshtml");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("SsAdmin");
            return View("Views/Login/Login.cshtml");
        }
    }

}
