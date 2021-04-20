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
        [Route("users")]
        [Route("users/index")]
        [HttpGet]
        public IActionResult Index()
        {
            
            return View ("Views/BackEnd/Users/Index.cshtml");
        }

    }
}
