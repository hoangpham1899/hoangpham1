using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class AccountsController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if(ModelState.IsValid)
            {
                user.Role = 0;
                user.Token = Guid.NewGuid().ToString();
                db.Users.Add(user);
                db.SaveChanges();
                Session["user"] = Session["flag"] = user;
            }
            return Redirect("/Home/Index");
        }
        [HttpPost]
        public JsonResult checkEmail(string Email)
        {
            bool isExits = db.Users.FirstOrDefault(t => t.Email == Email) != null;
            return Json(!isExits, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(FormCollection f)
        {
            string sAcc = f["txtUserName"].ToString();
            string sPas = f["txtPassword"].ToString();
            User users = db.Users.Where(t => t.Role == 0).FirstOrDefault(t => t.Email == sAcc && t.Password == sPas);
            User admin = db.Users.Where(t => t.Role == 1).FirstOrDefault(t => t.Email == sAcc && t.Password == sPas);
            if(admin != null)
            {
                Session["admin"] = admin;
                Session["flag"] = admin;
                return Redirect("/Admin/Admin/Index");
            }
            else if(users != null)
            {
                Session["user"] = users;
                Session["flag"] = users;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SignOut()
        {
            Session["admin"] = Session["user"] = Session["flag"] = null;
            return Redirect("/Home/Index");
        }
    }
}