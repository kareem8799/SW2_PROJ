using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.ViewModel
{
    public class AdsCategoryViewModel
    {
        public IEnumerable<Categories> Category { get; set; }
        public Ads Ad { get; set; }
    }
}