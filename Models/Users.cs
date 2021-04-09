using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string name { get; set; }
        public string level { get; set; }
    }
}
