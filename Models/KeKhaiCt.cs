using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class KeKhaiCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }        
        public string Describe { get; set; }

    }
}
