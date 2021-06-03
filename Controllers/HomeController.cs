using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                return Redirect("/Login");
            }
            else
            {
                ViewData["ssa"] = HttpContext.Session.GetString("SsaAdmin");
                return View("Views/Home/Index.cshtml");
            }
        }

        [Route("Json")]
        [HttpGet]
        public IActionResult JsonEdit()
        {
            var model = _db.Permission.Where(p => p.Username == "minhtran" && p.Roler == "user").First();
            return Ok(model);
            //    return View("Views/Home/Json.cshtml");
        }

        [Route("JsonUpdate")]
        [HttpPost]
        public IActionResult JsonUpdate()
        {
            var data = Request.Form["roler.user.index"];
            return Ok(data);
        }
        
        [Route("View")]
        [HttpGet]
        public IActionResult ViewFB()
        {

            return View("Views/FrontEnd/Home/Index.cshtml");
        }
    }   
}
