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
        [Required] 
        [MinLength(6)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]      
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Phone]
        [MinLength(10)]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(5)]
        public string Level { get; set; }
        [Required]
        [MaxLength(5)]
        public string Sadmin { get; set; }
        public string Permission { get; set; }    

    }
}
