using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.ViewModel;
using System.IO;
using System.Data.Entity;

namespace WebApplication5.Controllers
{
    public class AdsController : Controller
    {
        private ApplicationDbContext _context;
        public AdsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Ads
        public ActionResult ViewAllAds(string Searching)
        {

            return View(_context.Ads_db.Where(x => x.ADs_Tittle.Contains(Searching) || Searching == null).ToList());
        }

        
        public ActionResult ViewOneAdOnly(int IdOfProduct)
        {
            var result = _context.Ads_db.SingleOrDefault(m => m.ADs_ID == IdOfProduct);

            return View(result);
        }


        [HttpGet]
        public ActionResult Add_Ads()
        {
            var categories = _context.Category_db.ToList();
            var viewmodel = new AdsCategoryViewModel
            {
                Category = categories
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddadsDB(AdsCategoryViewModel Ad1, HttpPostedFileBase imgFile)
        {
            string x = Session["username"].ToString();
            var result = _context.User_db.Where(m => m.User_Name == x).First();
            Ad1.Ad.User_id = result.User_ID;
            
            if (imgFile != null)
            {
                string path1 = Path.Combine(Server.MapPath("~/Uploads"), imgFile.FileName);
                imgFile.SaveAs(path1);
                Ad1.Ad.Ad_image = imgFile.FileName;
                _context.Ads_db.Add(Ad1.Ad);
                _context.SaveChanges();
                var resultofcateg = _context.Category_db.Where(m => m.Categories_ID == Ad1.Ad.Ads_Categories).First();
                resultofcateg.number_of_products++;
                _context.Entry(resultofcateg).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ViewAllAds", "Ads");
            }

            return View();
        }

        public ActionResult UpdateAd(int IdOfProduct)
        {
            var categories = _context.Category_db.ToList();
            var result = _context.Ads_db.Where(m => m.ADs_ID == IdOfProduct).First();
            var viewmodel = new AdsCategoryViewModel
            {
                Category = categories,
                Ad = result

            };
            return View(viewmodel);
        }
        public ActionResult UpdateAdDB(AdsCategoryViewModel Ad1, HttpPostedFileBase imgFile)
        {

            if (imgFile != null)
            {
                string path1 = Path.Combine(Server.MapPath("~/Uploads"), imgFile.FileName);
                imgFile.SaveAs(path1);
                Ad1.Ad.Ad_image = imgFile.FileName;
                _context.Entry(Ad1.Ad).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("ViewOneAdForUser", "Account", new { IdOfProduct = Ad1.Ad.ADs_ID });
            }
            return RedirectToAction("ViewOneAdForUser", "Account", new { IdOfProduct = Ad1.Ad.ADs_ID });
        }


        public ActionResult DeleteAdDB(int IdOfAd)
        {

            var result = _context.Ads_db.SingleOrDefault(m => m.ADs_ID == IdOfAd);
            if (result != null)
            {
                _context.Ads_db.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction("ShowMyAds", "Account", new { username = Session["username"] });
        }


    }
}