using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class CartAd
    {

        [Key,Column(Order=0)]
        [ForeignKey("cart")]
        public int CartId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("ad")]
        public int AdId { get; set; }

        public virtual Cart cart { get; set; }
        public virtual Ads ad { get; set; }
    }
}