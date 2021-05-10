using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public UsersController(ApplicationDbContext db)
        {
            _db = db;            
        }

        [Route("Users")]
        [Route("Users/Index")]
        [HttpGet]
        public IActionResult Index(string level)
        {
            IEnumerable<Users> model = _db.Users;
            if (!string.IsNullOrEmpty(level) && level != "all")
            {
                model = model.Where(u => u.Level == level);
            }
            ViewData["level"] = level;
            ViewData["Title"] = "Users";
            return View("Views/BackEnd/Users/Index.cshtml", model);
        }

        [Route("Users/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Views/BackEnd/Users/Create.cshtml");
        }

        [Route("Users/Store")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Users user)
        {
            if (!ModelState.IsValid)
            {
                string md5_password = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    string change = GetMd5Hash(md5Hash, user.Password);
                    md5_password = change;
                }
                user.Password = md5_password;
                user.Sadmin = "";
                user.Level = "T";
                var model = _db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (model != null)
                {
                    ModelState.AddModelError("UserName", "Username Duplicate");
                    return View("Views/BackEnd/Users/Create.cshtml", user);
                }
                else
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Redirect("/Users");
                }
            }
            else
            {
                return View("Views/BackEnd/Users/Create.cshtml", user);
            }
        }

        [Route("Users/Edit/{id?}")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var model = _db.Users.Find(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View("Views/BackEnd/Users/Edit.cshtml", model);
            }
        }

        [Route("Users/Update")]
        [HttpPost]
        public IActionResult Update(Users request)
        {
            if (!ModelState.IsValid)
            {
                request.Level = "T";
                _db.Users.Update(request);
                _db.SaveChanges();
                return Redirect("/users");
            }
            else
            {
                return View("Views/BackEnd/Users/Create.cshtml", request);
            }

        }

        [Route("Users/Delete")]
        [HttpPost]
        public IActionResult Delete(int iddelete)
        {
            var model = _db.Users.FirstOrDefault(u => u.Id == iddelete);
            _db.Users.Remove(model);
            _db.SaveChanges();

            return Redirect("/Users");
        }



        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
