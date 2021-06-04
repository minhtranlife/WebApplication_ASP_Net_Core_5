using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Users
    {
        [Key]
        [Display(Name="Id")]
        public int Id { get; set; }

        [Required,]        
        [RegularExpression(@"^(?=.{6,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$"
            , ErrorMessage="Username error")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]       
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"
            , ErrorMessage="Password error")]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]           
        [Display(Name = "BirthDay")]
        public DateTime BirthDay { get; set; }
                
        [MaxLength(5)]
        [Display(Name = "Level")]
        public string Level { get; set; }   
        
        [MaxLength(5)]
        [Display(Name = "Sadmin")]
        public string Sadmin { get; set; }

        [Display(Name = "Permission")]
        public string Permission { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [NotMapped]
        public IFormFile AvatarFile { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }              

    }
}

//Username
//^(?=.{ 3,32}$)(? !.*[._ -]{ 2})(? !.*[0 - 9]{ 5,})[a-z](?:[\w] *|[a - z\d\.] *|[a - z\d -] *)[a - z0 - 9]$
//tên người dùng phải bắt đầu bằng [a-z]
//tên người dùng phải kết thúc bằng [a-z0-9]
//tên người dùng có thể có độ dài từ 3 đến 32
//tên người dùng có thể chứa bất kỳ [a-z0-9\._-]
//Các số không được ở gần nhau hơn 4 lần. Ý tôi p1234là một trận đấu và p12345không.
//mỗi tên người dùng chỉ có thể chứa một trong số [\._-]. Ý tôi là tên người dùng có thể chứa .hoặc -hoặc_
//mỗi ., -và _phải được theo sau bởi một chữ cái-số. Ý tôi là một .không thể được theo sau bởi một cái khác .. Chúng không nên ở gần nhau.