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
    public class SearchController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuPet()
        {
            return View(db.Species.ToList());
        }
        public ViewResult Keyword(int? page, string Key)
        {
            if(Key != null)
            {
                Session["key"] = Key;
            }
            string key = Session["key"].ToString();
            List<Pet> pet = db.Pets.Where(t => t.PetName.Contains(key)).OrderBy(t => t.PetName).ToList();
            ViewBag.TB = "Có " + pet.Count + " kết quả";
            return View(pet.ToPagedList(page ?? 1, 30));
        }
        public ActionResult PetofSpecies(int? id, int? page)
        {
            return View(db.Pets.Where(t => t.IDSpecies == id).OrderBy(t => t.PetName).ToPagedList(page ?? 1, 30));
        }
    }
}