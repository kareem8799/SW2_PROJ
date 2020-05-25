using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication5.Models
{
    public class Ads
    {
        [Key]
        public int ADs_ID { get; set; }

        [Required(ErrorMessage = "this field is requried")]
        public string ADs_Tittle { get; set; }

        [Required(ErrorMessage = "this field is requried")]
        public string ADs_Discription { get; set; }

        [Required(ErrorMessage = "this field is requried")]
        public Boolean ADs_State { get; set; }

        [Required(ErrorMessage = "this field is requried")]
        public float ADs_Rating { get; set; }

        [ForeignKey("userid")]
        [Required(ErrorMessage = "this field is requried")]
        public int User_id { get; set; }
        public virtual User userid { get; set; }

        [Required(ErrorMessage = "this field is requried")]
        public string Ad_image { get; set; }

        [ForeignKey("category")]
        [Required(ErrorMessage = "this field is requried")]
        public int Ads_Categories { get; set; }
        public virtual Categories category { get; set; }

        public virtual ICollection<CartAd> cartad { get; set; }
    }
}