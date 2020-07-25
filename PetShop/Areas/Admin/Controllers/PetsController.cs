using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;
using System.IO;

namespace PetShop.Areas.Admin.Controllers
{
    public class PetsController : Controller
    {
        private DBPetShopEntities db = new DBPetShopEntities();

        // GET: Admin/Pets
        public ActionResult Index()
        {
            var pets = db.Pets.Include(p => p.Species);
            return View(pets.ToList());
        }

        // GET: Admin/Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Admin/Pets/Create
        public ActionResult Create()
        {
            ViewBag.IDSpecies = new SelectList(db.Species, "IDSpecies", "SpeciesName");
            return View();
        }

        // POST: Admin/Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPet,PetName,Sex,Descreptions,Age,IDSpecies,Image,DateCreate,Price,Views,Rate,Vote,Summary")] Pet pet, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                var img = Path.GetFileName(fileimg.FileName);
                var pathimg = Path.Combine(Server.MapPath("~/Content/images"), img);
                if (fileimg == null)
                {
                    ViewBag.Img = "Chose images";
                    return View();
                }
                else if (System.IO.File.Exists(pathimg))
                    ViewBag.Img = "Images had exists";
                else
                    fileimg.SaveAs(pathimg);
                pet.Image = fileimg.FileName;
                pet.DateCreate = DateTime.Now;
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSpecies = new SelectList(db.Species, "IDSpecies", "SpeciesName", pet.IDSpecies);
            return View(pet);
        }

        // GET: Admin/Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSpecies = new SelectList(db.Species, "IDSpecies", "SpeciesName", pet.IDSpecies);
            return View(pet);
        }

        // POST: Admin/Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPet,PetName,Sex,Descreptions,Age,IDSpecies,Image,DateCreate,Price,Views,Rate,Vote,Summary")] Pet pet, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                var img = Path.GetFileName(fileimg.FileName);
                var pathimg = Path.Combine(Server.MapPath("~/Content/images"), img);
                if (fileimg == null)
                {
                    ViewBag.Img = "Chose images";
                    return View();
                }
                else if (System.IO.File.Exists(pathimg))
                    ViewBag.Img = "Images had exists";
                else
                    fileimg.SaveAs(pathimg);
                pet.Image = fileimg.FileName;
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDSpecies = new SelectList(db.Species, "IDSpecies", "SpeciesName", pet.IDSpecies);
            return View(pet);
        }

        // GET: Admin/Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Admin/Pets/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
