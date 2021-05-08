using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Data;
using System.Text.Encodings.Web;


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
        public IActionResult Index(string level )
        {
            IEnumerable<Users> model = _db.Users;
            if (!string.IsNullOrEmpty(level) && level != "all")
            {
                model = model.Where(u => u.Level == level);
            }
            ViewData["level"] = level;            
            return View ("Views/BackEnd/Users/Index.cshtml", model);
        }

        [Route("Users/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Views/BackEnd/Users/Create.cshtml");
        }

        [Route("Users/Store")]
        [HttpPost]
        public IActionResult Store(Users user)
        {
            if (!ModelState.IsValid)
            {
                user.Sadmin = "";
                user.Level = "T";
                _db.Users.Add(user);
                _db.SaveChanges();
                return Redirect("/Users");                
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
                if(model == null)
                {
                    return NotFound();
                }
                return View("Views/BackEnd/Users/Edit.cshtml", model);
            }  
        }

        [Route("Users/Update")]
        [HttpPost]
        public IActionResult Update(Users user)
        {
            if (!ModelState.IsValid)
            {
                user.Sadmin = "";
                _db.Users.Update(user);
                _db.SaveChanges();
                return Redirect("/Users");
            }
            else
            {
                return View("Views/BackEnd/Users/Create.cshtml", user);
            }

        }

        [Route("Users/Delete")]
        [HttpPost]
        public IActionResult Delete(int iddelete)
        {
            var model = _db.Users.Find(iddelete);
            _db.Users.Remove(model);
            _db.SaveChanges();

            return Redirect("/Users");
        }
    }
}
