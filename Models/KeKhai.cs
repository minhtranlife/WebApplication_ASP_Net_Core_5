using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class KeKhai
    {
        [Key]
        public int Id { get;set;}

        [Required]
        public string mahs { get; set; }

        [Required]
        public string mota { get; set; }
    }
}
