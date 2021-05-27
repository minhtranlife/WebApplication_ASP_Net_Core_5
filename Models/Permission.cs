using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Permission
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }         
        public string Username { get; set; }
        public string Roler { get; set; }
        public bool Index { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }

    }
}
