using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public UsersController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("Users")]
        [Route("Users/Index")]
        [HttpGet]
        public IActionResult Index(string Level, string Name, int Page, int PageSize)
        {
            if (Page == null || Page < 1)
            {
                Page = 1;
            }
            if (PageSize == null || PageSize < 1)
            {
                PageSize = 5;
            }
            if (PageSize > 20)
            {
                PageSize = 20;
            }
            IEnumerable<Users> model = _db.Users;
            if (!string.IsNullOrEmpty(Level) && Level != "all")
            {
                model = model.Where(u => u.Level == Level);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                model = model.Where(u => u.Name.Contains(Name));
            }
            int count = model.Count();
            model = model.Skip((Page - 1) * PageSize)
                    .Take(PageSize).ToList();

            int TotalRecords = count;
            int TotalPages = Convert.ToInt32(Math.Ceiling((double)TotalRecords / (double)PageSize));
            int NextPage = Page + 1;
            int PreviousPage = Page - 1;

            ViewData["Level"] = Level;
            ViewData["Name"] = Name;
            ViewData["Title"] = "Users";
            ViewData["TotalPages"] = TotalPages;
            ViewData["TotalRecords"] = TotalRecords;
            ViewData["PageSize"] = PageSize;
            ViewData["Page"] = Page;

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
                var model = _db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (model != null)
                {
                    ModelState.AddModelError("UserName", "Username Duplicate");
                    return View("Views/BackEnd/Users/Create.cshtml", user);
                }
                else
                {
                    if (user.AvatarFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(user.AvatarFile.FileName);
                        string extension = Path.GetExtension(user.AvatarFile.FileName);
                        //return Ok(extension);
                        user.Avatar = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/avatar/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            user.AvatarFile.CopyToAsync(FileStream);
                        }
                    }
                    else
                    {
                        user.Avatar = "default-user.png";
                    }
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
        public IActionResult Update(Users request, string newPassWord)
        {

            if (!ModelState.IsValid)
            {
                if (request.AvatarFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(request.AvatarFile.FileName);
                    string extension = Path.GetExtension(request.AvatarFile.FileName);
                    //return Ok(extension);
                    request.Avatar = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/avatar/", filename);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        request.AvatarFile.CopyToAsync(FileStream);
                    }
                }
                if (newPassWord != null)
                {
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = GetMd5Hash(md5Hash, newPassWord);
                        md5_password = change;
                    }
                    request.Password = md5_password;
                }
                _db.Users.Update(request);
                _db.SaveChanges();
                return Redirect("/users");
            }
            else
            {
                return View("Views/BackEnd/Users/Edit.cshtml", request);
            }
        }

        [Route("Users/Delete")]
        [HttpPost]
        public IActionResult Delete(int iddelete)
        {
            var model = _db.Users.FirstOrDefault(u => u.Id == iddelete);
            //delete img from wwwroot/image/avatar
            if (model.Avatar != "default-user.png")
            {
                var imgPath = Path.Combine(_hostEnvironment.WebRootPath, "images/avatar", model.Avatar);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }

            }
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
