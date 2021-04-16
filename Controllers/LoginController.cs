﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("login")]
        public IActionResult Login()
        {            
            return View("Views/Login/Login.cshtml");
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult Signin(string username, string password)
        {
            var models = _context.Users.Where(p => p.Username == username).First();
            if (models != null) {                
                if (models.Password == password)
                {
                    HttpContext.Session.SetString("username", username);
                    return View("Views/Home/Index.cshtml");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Views/Login/Login.cshtml");
                }
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
            HttpContext.Session.Remove("username");
            return Redirect("Home");
        }
    }
}
