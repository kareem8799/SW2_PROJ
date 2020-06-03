using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;


namespace WebApplication5.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public CartController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Cart
        public ActionResult AdToCart(int idofAd)
        {
            string x = Session["username"].ToString();
            var result = _context.User_db.Where(m => m.User_Name == x).First();

            var result1 = _context.CartAd_db.SingleOrDefault(m => m.CartId == result.User_ID && m.AdId == idofAd);

            if (result1 == null)
            {
                var NewCartId = new CartAd
                {
                    CartId =result.User_ID,
                    AdId = idofAd
                };
                _context.CartAd_db.Add(NewCartId);
                _context.SaveChanges();

                return RedirectToAction("ViewCategory", "Admin");
            }
            else
            {
                return RedirectToAction("ViewCategory", "Admin");
            }
         
        }
        public ActionResult RemoveAdfromCart(int idofad)
        {
            string x = Session["username"].ToString();
            var result = _context.User_db.Where(m => m.User_Name == x).First();

            var result1 = _context.CartAd_db.SingleOrDefault(m => m.AdId == idofad && m.CartId==result.User_ID);
            if (result1 != null)
            {
                _context.CartAd_db.Remove(result1);
                _context.SaveChanges();
            }
           return RedirectToAction("home", "Account");
        }


    //    public ActionResult ViewAllCart()
    //    {
    //        string x = Session["username"].ToString();
    //        var result = _context.User_db.Where(m => m.User_Name == x).First();

    //        List<Ads> result2 = new List<Ads>();
    //        var result1 = _context.CartAd_db.Where(m=>m.CartId==result.User_ID);
    //        foreach (var zz in result1)
    //        {
    //            Ads cf= fromdb(zz.AdId);
    //            result2.Add(cf);
    //        }
    //        return View(result2);
    //    }
    //    public Ads fromdb(int x)
    //    {
    //        var dx = _context.Database.Connection.Close().Ads_db.Where(m => m.ADs_ID == x).First();
    //        _context.Database.Connection.Close();
    //        return dx;
    //    }
   }
}