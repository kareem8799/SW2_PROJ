using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
         
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategoryDB(Categories category)
        {
            _context.Category_db.Add(category);
            _context.SaveChanges();
            return RedirectToAction("LoginForAdmin", "Account");
        }
        public ActionResult ViewCategory()
        {
            var result = _context.Category_db.ToList();
            return View(result);
        }
        public ActionResult RemoveCategory(int idofcategory)
        {
            var result = _context.Category_db.SingleOrDefault(m => m.Categories_ID == idofcategory);
            if (result != null)
            {
                _context.Category_db.Remove(result);
                _context.SaveChanges();
            }
              return RedirectToAction("LoginForAdmin","Account");
        }
        public ActionResult ViewAdsForAdmin()
        {

            return View(_context.Ads_db.ToList());
        }

        public ActionResult Dis_ActiveAd(int idofad)
        {
            var result = _context.Ads_db.SingleOrDefault(m => m.ADs_ID == idofad);
            if (result != null)
            {
                if (result.ADs_State == true)
                {
                    result.ADs_State = false;
                    _context.SaveChanges();
                }
                else
                {
                    result.ADs_State = true;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("LoginForAdmin", "Account");
        }
        public ActionResult ViewUserForAdmin()
        {
            return View(_context.User_db.ToList());
        }
        public ActionResult Dis_blockUser(int idofuser)
        {
            var result = _context.User_db.SingleOrDefault(m=>m.User_ID==idofuser);
            if (result != null)
            {
                if (result.User_State == true)
                {
                    result.User_State = false;
                    _context.SaveChanges();
                }
                else
                {
                    result.User_State = true;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("LoginForAdmin", "Account");
        }
    }
}