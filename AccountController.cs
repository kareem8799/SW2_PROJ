using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        
        private ApplicationDbContext _context;
        public AccountController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Accounts
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterDb(User DataofUser)
        {
            _context.User_db.Add(DataofUser);
            _context.SaveChanges();
            
            return View();
        }

        
        public ActionResult Login()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("home", "Account", new { username = Session["username"] });
            }
            else {
                return View();
            }
            
        }

        public ActionResult LoginForAdmin(string username)
        {
            return View();
        }
        
        public ActionResult loginDb(User DataofUser)
        {
            
            var resultForUser  = _context.User_db.SingleOrDefault(m => m.User_Email == DataofUser.User_Email && m.password == DataofUser.password);
            var resultForAdmin = _context.Admin_db.SingleOrDefault(m => m.Admin_UserName == DataofUser.User_Email && m.Admin_Pass == DataofUser.password);
            
                if (resultForAdmin != null && resultForAdmin.RoleModel == 1)
                {
                    Session["username"] = resultForAdmin.Admin_UserName;
                    return RedirectToAction("LoginForAdmin", "Account", new { username = resultForAdmin.Admin_UserName });
                }
                else if (resultForUser != null && resultForUser.RoleModel ==2 )
                {
                    if(resultForUser.User_State == true )
                    {
                        Session["username"] = resultForUser.User_Name;
                        var result2 = _context.Cart_db.SingleOrDefault(m => m.idofcart == resultForUser.User_ID);
                        if (result2 == null)
                        {
                        var cartforuser = new Cart { idofcart = resultForUser.User_ID };
                        _context.Cart_db.Add(cartforuser);
                        _context.SaveChanges();
                        }
                        return RedirectToAction("ViewAdsForUser", "Account", new { username = resultForUser.User_Name });
                    }
                    else
                    {
                        return View();
                    }
                }
               else
               {
                ViewBag.notvaild = "password is not vaild";
                return RedirectToAction("Login");
               }
      }


        public ActionResult logout()
        {
            Session.Remove("username");
            Session.RemoveAll();
            return RedirectToAction("ViewAllAds", "Ads");
        }


        public ActionResult home(string username)
        {

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else{
                string x = Session["username"].ToString();
                var result = _context.User_db.Where(m => m.User_Name == x).First();
                
            return View(result);
            }
        }
        public ActionResult UserProfile(string username)
        {
            string x = Session["username"].ToString();
            var result = _context.User_db.Where(m => m.User_Name == x).First();
            return View(result);
        }
        public ActionResult UpdataUserProfile(int idofuser)
        {
            var result = _context.User_db.Where(m => m.User_ID == idofuser).First();
            return View(result);
        }
        public ActionResult UpdataUserProfileDB(User userdata)
        {
                _context.Entry(userdata).State = EntityState.Modified;
                _context.SaveChanges();   
            return RedirectToAction("UserProfile", "Account");
        }
        public ActionResult ShowMyAds(string username)
        {
            string x = Session["username"].ToString();
            var result = _context.User_db.Where(m => m.User_Name == x).First();
            var result1 = _context.Ads_db.Where(m => m.User_id == result.User_ID);
            return View(result1);
        }
        public ActionResult ViewAdsForUser(string CategName)
        {

            return View(_context.Ads_db.Where(x => x.ADs_Tittle.Contains(CategName) || CategName == null).ToList());
        }
       
        public ActionResult  ViewOneAdForUser(int IdOfProduct)
        {
            var result = _context.Ads_db.SingleOrDefault(m => m.ADs_ID == IdOfProduct);

            return View(result);
        }

    }
}