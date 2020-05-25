using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Categories
    {
        [Key]
        public int Categories_ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "catrgory")]
        public string Categories_Name { get; set; }

        public int number_of_products { get; set; }

        public virtual ICollection<Ads> ads { get; set; }
    }
}