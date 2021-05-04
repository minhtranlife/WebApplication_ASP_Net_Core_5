using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using WebApplication.Models;
using WebApplication.Data;
using Newtonsoft.Json;

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
                
                // Error when user not in db
                try 
                {
                    var model = _db.Users.Where(u => u.Username == username).First();
                    if (model.Password == password)
                    {
                        HttpContext.Session.SetString("name", model.Name);
                        HttpContext.Session.SetString("username", model.Username);
                        HttpContext.Session.SetString("permission", model.Permission);
                        HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));


                        return Redirect("/");
                    }
                    else
                    {
                        ViewBag.error = "Invalid Password";
                        return View("Views/Login/Login.cshtml");
                    }
                }
                catch
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
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("permission");
            return Redirect("/Login");
        }    
    }
}
