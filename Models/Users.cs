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
        [Display(Name="Id")]
        public int Id { get; set; }
        [Required] 
        [MinLength(6)]
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]      
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Phone]
        [MinLength(10)]
        [MaxLength(10)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        [Display(Name = "Level")]
        public string Level { get; set; }       
        [MaxLength(5)]
        [Display(Name = "Sadmin")]
        public string Sadmin { get; set; }
        [Display(Name = "Permission")]
        public string Permission { get; set; }   
    }
}
