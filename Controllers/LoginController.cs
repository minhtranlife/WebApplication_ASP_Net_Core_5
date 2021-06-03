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
using System.Security.Cryptography;

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

                try
                {
                    var model = _db.Users.Where(u => u.Username == username).First();
                    
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = Global_Funtions.GetMd5Hash(md5Hash, password);
                        md5_password = change;
                    }                  
                    if (model.Password == md5_password)
                    {
                        var permission = _db.Permission.Where(p => p.Username == model.Username);


                        HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));
                        HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permission));
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
            HttpContext.Session.Remove("SsAdmin");
            return Redirect("/Login");
        }

        //static string GetMd5Hash(MD5 md5Hash, string input)
        //{

        //    // Convert the input string to a byte array and compute the hash.
        //    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        //    // Create a new Stringbuilder to collect the bytes
        //    // and create a string.
        //    StringBuilder sBuilder = new StringBuilder();

        //    // Loop through each byte of the hashed data
        //    // and format each one as a hexadecimal string.
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }

        //    // Return the hexadecimal string.
        //    return sBuilder.ToString();
        //}
    }
}
