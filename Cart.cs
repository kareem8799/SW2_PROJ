using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("user")]
        public int idofcart { get; set; }
        
        public virtual User user { get; set; }
        public virtual ICollection<CartAd> cartad { get; set; }
    }
}