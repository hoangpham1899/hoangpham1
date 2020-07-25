using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Areas.Admin.Controllers
{
    public class CountController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: Admin/Count
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CountUser()
        {
            List<User> Dem = db.Users.ToList();
            ViewBag.ThongBao = Dem.Count;
            return PartialView();
        }
        public PartialViewResult CountPet()
        {
            List<Pet> Dem = db.Pets.ToList();
            ViewBag.ThongBao = Dem.Count;
            return PartialView();
        }
        public PartialViewResult CountOrder()
        {
            List<Order> Dem = db.Orders.ToList();
            ViewBag.ThongBao = Dem.Count;
            return PartialView();
        }
        public PartialViewResult CountFeedback()
        {
            List<Feedback> Dem = db.Feedbacks.ToList();
            ViewBag.ThongBao = Dem.Count;
            return PartialView();
        }
        public PartialViewResult CountSpecies()
        {
            List<Species> Dem = db.Species.ToList();
            ViewBag.ThongBao = Dem.Count;
            return PartialView();
        }
    }
}