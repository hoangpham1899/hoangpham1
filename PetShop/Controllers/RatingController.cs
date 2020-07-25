using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class RatingController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: Rating
        public ActionResult Index(int? id, int? rate)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            Pet pet = db.Pets.Find(id);
            float tmp = (float)pet.Rate;
            Int32 tmpvote = (Int32)pet.Vote;
            pet.Rate = ((tmp * tmpvote) + rate) / (tmpvote + 1);
            pet.Vote++;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}