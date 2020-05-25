using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }


        [Display(Name="Name")]
        [EmailAddress]
        [Required(ErrorMessage = "this field is requried")]
        public string Admin_UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "this field is requried")]
        public string Admin_Pass { get; set; }

        public int RoleModel { get; set; }

        public Admin()
        {
            RoleModel = 1;
        }
    }
}