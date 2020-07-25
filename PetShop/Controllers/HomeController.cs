using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;
using PagedList;
using PagedList.Mvc;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: Home
        public ActionResult Index()
        {
            if(Session["count"] == null)
            {
                Session["count"] = "Trống";
            }
            return View();
        }
        public PartialViewResult Tool()
        {
            return PartialView();
        }
        public PartialViewResult NewPet()
        {
            return PartialView(db.Pets.OrderByDescending(t => t.DateCreate).Take(6).ToList());
        }
       
        public PartialViewResult HotPet()
        {
            return PartialView(db.Pets.OrderByDescending(t => t.Views).Take(6).ToList());
        }
       
        public ActionResult TopPet(int? page)
        {
            return View(db.Tops.OrderByDescending(t => t.Quanity).ToPagedList(page ?? 1,30));
        }
        public ActionResult RatingPet(int? page)
        {
            return View(db.Pets.OrderByDescending(t => t.Rate).ToPagedList(page ?? 1, 30));
        }
        public ActionResult AllPet(int? page)
        {
            return View(db.Pets.OrderBy(t => t.PetName).ToPagedList(page ?? 1, 30));
        }
        public PartialViewResult Menu()
        {
            return PartialView();
        }
        public ActionResult DetailsPet(int? id)
        {
            Session["idpet"] = id;
            return View(db.Pets.Find(id));
        }
        public PartialViewResult SliderPet()
        {
            return PartialView(db.Pets.OrderByDescending(t => t.Rate).Take(5).ToList());
        }
    }
}