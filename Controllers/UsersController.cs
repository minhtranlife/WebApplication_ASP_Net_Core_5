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


        [Route("users")]
        [Route("users/index")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewData.Model = _db.Users;  
            
            return View ("Views/BackEnd/Users/Index.cshtml", ViewData);
        }

    }
}
