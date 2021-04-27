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

        [Route("")]
        public IActionResult Index()
        {            
            return View("Views/Login/Login.cshtml");
        }

        [Route("login")]
        [HttpPost]
        public IActionResult LoginApp(string username, string password)
        {
            if (username != null && password != null)
            {
               
                //if (username != null)
                //{
                //    if (userInfo.Password == password)
                //    {
                        //HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(userInfo));
                        return Redirect("/");
                //    }
                //    else
                //    {
                //        ViewBag.error = "Invalid Account";
                //        return View("Views/Login/Login.cshtml");
                //    }
                //}
                //else
                //{
                //    ViewBag.error = "Invalid Account";
                //    return View("Views/Login/Login.cshtml");
                //}

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
