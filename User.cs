using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "this field is requried")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "this field is requried")]
        [Compare("password")]
        public string confrim_password { get; set; }


        [Display(Name = "Name")]
        public string User_Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "this field is requried")]
        public string User_Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "this field is requried")]
        public string User_phone { get; set; }


        public bool User_State { get; set; }

        public int RoleModel { get; set; }
        public virtual ICollection<Ads> ads { get; set; }
        public virtual Cart cartuser { get; set; }

        public User()
        {
            User_State = true;
            RoleModel = 2;
        }
    }
   
}