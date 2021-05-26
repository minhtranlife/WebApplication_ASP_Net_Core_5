using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers.KeKhaiGia
{
    public class KeKhaiGiaCtController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KeKhaiGiaCtController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("KeKhaiGiaCt/store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Describe)
        {
            if (string.IsNullOrEmpty(Describe))
            {
                var data = new { status = false, message = "" };
                return Json(data);
            }
            else
            {
                var request = new KeKhaiCt { Describe = Describe, Mahs = Mahs };
                _db.KeKhaiCt.Add(request);
                _db.SaveChanges();

                var model = _db.KeKhaiCt.Where(ct => ct.Mahs == Mahs).ToList();

                string result = "<div class='row' id='hsct'>";
                result += "<div class='col-md-12'>";
                result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
                result += "<thead>";
                result += "<tr>";
                result += "<th style='text-align: center' width='2%'>STT</th>";
                result += "<th style='text-align: center' width='10%'>Ma Hs</th>";
                result += "<th style='text-align: center'>Mo Ta</th>";
                result += "<th style='text-align: center' width='25%'>Action</th>";
                result += "</tr>";
                result += "</thead>";
                result += "<tbody>";
                foreach (var item in model)
                {
                    result += "<tr>";
                    result += "<td>" + item.Id + "</td>";
                    result += "<td>" + item.Mahs + "</td>";
                    result += "<td>" + item.Describe + "</td>";
                    result += "<td>";
                    result += "<button type='button' data-target='#modal-edit' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='Edit(" + item.Id + ")'>";
                    result += "<i class='fa fa-edit'></i>&nbsp;Edit";
                    result += "</button>";
                    result += "<button type='button' data-target='#modal-delete' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='getId(" + item.Id + ")'>";
                    result += "<i class='fa fa-trash-o'></i>&nbsp;Delete";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                }
                result += "</tbody>";
                result += "</table>";
                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
        }

        [Route("KeKhaiGiaCt/Delete")]
        [HttpPost]
        public JsonResult Delete(string Id, string Mahs)
        {
           
            if (string.IsNullOrEmpty(Id))
            {
                var data = new { status = false, message = "" };

                return Json(data);
            }
            else
            {
                int iddelete = Int32.Parse(Id);
                var model = _db.KeKhaiCt.FirstOrDefault(u => u.Id == iddelete);
                _db.KeKhaiCt.Remove(model);
                _db.SaveChanges();

                var modelct = _db.KeKhaiCt.Where(ct => ct.Mahs == Mahs).ToList();

                //string result = "<h1>";
                //foreach (var item in model)
                //{
                //    //result += item.Mahs + "-" + item.Describe + "<br>";

                //}
                //result += "</h1>";
                string result = "<div class='row' id='hsct'>";
                result += "<div class='col-md-12'>";
                result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
                result += "<thead>";
                result += "<tr>";
                result += "<th style='text-align: center' width='2%'>STT</th>";
                result += "<th style='text-align: center' width='10%'>Ma Hs</th>";
                result += "<th style='text-align: center'>Mo Ta</th>";
                result += "<th style='text-align: center' width='25%'>Action</th>";
                result += "</tr>";
                result += "</thead>";
                result += "<tbody>";
                foreach (var item in modelct)
                {
                    result += "<tr>";
                    result += "<td>" + item.Id + "</td>";
                    result += "<td>" + item.Mahs + "</td>";
                    result += "<td>" + item.Describe + "</td>";
                    result += "<td>";
                    result += "<button type='button' data-target='#modal-edit' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='Edit(" + item.Id + ")'>";
                    result += "<i class='fa fa-edit'></i>&nbsp;Edit";
                    result += "</button>";
                    result += "<button type='button' data-target='#modal-delete' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='getId(" + item.Id + ")'>";
                    result += "<i class='fa fa-trash-o'></i>&nbsp;Delete";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                }
                result += "</tbody>";
                result += "</table>";
                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };

                return Json(data);
            }
        }

        [Route("KeKhaiGiaCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.KeKhaiCt.Find(Id);
            if(model != null)
            {
                string result = "<div class='modal-body' id='edit_ct'>";
                result += "<div class='row'>";
                result += "<div class='col-md-6'>";
                result += "<div class='form-group'>";
                result += "<label class='form-control-label'><b>ID:" + model.Id + "</b></label>";
                result += " <input id ='id_edit' name='id_edit' class='form-control' value='" + model.Id + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-md-6'>";
                result += "<div class='form-group'>";
                result += "<label class='form-control-label'><b>Mahs:" + model.Mahs + "</b></label>";
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "<div class='row'>";
                result += "<div class='col-md-12'>";
                result += "<div class='form-group'>";
                result += "<label class='form-control-label'><b>Describe</b><span class='require'>*</span></label>";
                result += " <input id ='Describe_edit' name='Describe_edit' class='form-control' value='" + model.Describe + "'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = false, message = "" };
                return Json(data);
            }
        }

        [Route("KeKhaiGiaCt/Update")]
        [HttpPost]
        public JsonResult Update(string Describe, string Id, string Mahs)
        {
            if (string.IsNullOrEmpty(Describe) && string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(Mahs))
            {
                var data = new { status = false, message = "" };
                return Json(data);
            }
            else
            {
                int id_edit = Int32.Parse(Id);
                var model = _db.KeKhaiCt.Where(ct=>ct.Id == id_edit).First();
                model.Describe = Describe;
                _db.SaveChanges();

                var modelct = _db.KeKhaiCt.Where(ct => ct.Mahs == Mahs).ToList();
                
                string result = "<div class='row' id='hsct'>";
                result += "<div class='col-md-12'>";
                result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
                result += "<thead>";
                result += "<tr>";
                result += "<th style='text-align: center' width='2%'>STT</th>";
                result += "<th style='text-align: center' width='10%'>Ma Hs</th>";
                result += "<th style='text-align: center'>Mo Ta</th>";
                result += "<th style='text-align: center' width='25%'>Action</th>";
                result += "</tr>";
                result += "</thead>";
                result += "<tbody>";
                foreach (var item in modelct)
                {
                    result += "<tr>";
                    result += "<td>" + item.Id + "</td>";
                    result += "<td>" + item.Mahs + "</td>";
                    result += "<td>" + item.Describe + "</td>";
                    result += "<td>";
                    result += "<button type='button' data-target='#modal-edit' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='Edit(" + item.Id + ")'>";
                    result += "<i class='fa fa-edit'></i>&nbsp;Edit";
                    result += "</button>";
                    result += "<button type='button' data-target='#modal-delete' data-toggle='modal'";
                    result += "class='btn btn-default btn-xs mbs' onclick='getId(" + item.Id + ")'>";
                    result += "<i class='fa fa-trash-o'></i>&nbsp;Delete";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                }
                result += "</tbody>";
                result += "</table>";
                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
        }
    }
}

