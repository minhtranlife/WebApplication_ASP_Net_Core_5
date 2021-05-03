using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

       
        [Route("login")]
        public IActionResult Index()
        {            
            return View("Views/Login/Login.cshtml");
        }

        [Route("login/signin")]
        [HttpPost]
        public IActionResult SigninApp(string username, string password)
        {

            if (username != null && password != null)
            {
                var model = _db.Users.Where(u => u.Username == username).First();
                // Error when user not in db
                if (model != null)
                {
                    if(model.Password == password)
                    {
                        HttpContext.Session.SetString("SsAdmin", model.Name);
                        HttpContext.Session.SetString("username", model.Username);
                        return Redirect("/");
                    }
                    else
                    {
                        ViewBag.error = "Invalid Password";
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
                ViewBag.error = "Username & Password Required !!!";
                return View("Views/Login/Login.cshtml");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return View("Views/Login/Login.cshtml");
        }
    }

}
