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


namespace WebApplication.Controllers
{
    
    public class HomeController : Controller
    {
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
                return View("Views/Home/Index.cshtml");
            }
        }

        [Route("Json")]
        [HttpGet]
        public IActionResult JsonEdit()
        {
            return View("Views/Home/Json.cshtml");
        }

        [Route("JsonUpdate")]
        [HttpPost]
        public IActionResult JsonUpdate()
        {
           
            return Ok(ViewBag);
        }
    }
}
