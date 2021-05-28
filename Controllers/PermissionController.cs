using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PermissionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PermissionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("Permission")]
        [HttpGet]
        public IActionResult Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Redirect("Users");
            }
            else
            {
                var model = _db.Permission.Where(u => u.Username == username).ToList();
                ViewData["username"] = username;

                return View("Views/BackEnd/Users/Permission/Index.cshtml", model);
            }

        }

        [Route("Permission/Store")]
        [HttpPost]
        public IActionResult Store()
        {
            if (string.IsNullOrEmpty(Request.Form["roler"]) || string.IsNullOrEmpty(Request.Form["username"]))
            {
                return Redirect("Users");
            }
            else
            {
                string roler = Request.Form["roler"];
                string username = Request.Form["username"];
                bool index = (string.IsNullOrEmpty(Request.Form["index"])) ? false : true;
                bool edit = (string.IsNullOrEmpty(Request.Form["edit"])) ? false : true;
                bool create = (string.IsNullOrEmpty(Request.Form["create"])) ? false : true;
                bool delete = (string.IsNullOrEmpty(Request.Form["delete"])) ? false : true;
                bool approval = (string.IsNullOrEmpty(Request.Form["approval"])) ? false : true;

                var requests = new Permission
                {
                    Username = username, 
                    Roler = roler,
                    Index = index,
                    Edit = edit,
                    Create = create,
                    Delete = delete,
                    Approval = approval
                };
                _db.Permission.Add(requests);
                _db.SaveChanges();

                return Redirect("/Permission?&username=" + username);
            }

        }
    }
}
