using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers.KeKhaiGia
{
    public class KeKhaiGiaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KeKhaiGiaController(ApplicationDbContext db)
        {
            _db = db;
          
        }
        [Route("KeKhaiGia")]
        [Route("KeKhaiGia/Index")]
        [HttpGet]
        public IActionResult Index(string Mota)
        {
            ViewData["mota"] = Mota;
            ViewData["Title"] = "Kê khai giá";

            var model = from k in _db.KeKhai select k;
            if (!string.IsNullOrEmpty(Mota))
            {
                model = model.Where(k => k.mota.Contains(Mota));
            }
   
            return View("/Views/BackEnd/KeKhaiGia/Index.cshtml", model);
        }

        [Route("KeKhaiGia/{id}/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            KeKhai model = _db.KeKhai.Find(id);
            List<KeKhaiCt> modelct = (from ct in _db.KeKhaiCt where ct.Mahs == model.mahs select ct).ToList();

            KeKhaiCreEdi FinalModel = new KeKhaiCreEdi();
           
            FinalModel.Kekhai = model;
            FinalModel.KeKhaiCt = modelct;

            return View("/Views/BackEnd/KeKhaiGia/Edit.cshtml", FinalModel);
        }

        [Route("KeKhaiGia/Update")]
        [HttpPost]
        public IActionResult Update()
        {
            string Id = Request.Form["Kekhai.Id"];
            string Mahs = Request.Form["Kekhai.Mahs"];
            string Mota = Request.Form["Kekhai.mota"];
            var model = _db.KeKhai.Find(Convert.ToInt32(Id));
            model.mota = Mota;            
            _db.SaveChanges();

            return Redirect("/KeKhaiGia");            
        }
    }
}
