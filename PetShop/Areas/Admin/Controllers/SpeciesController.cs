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
    public class SpeciesController : Controller
    {
        private DBPetShopEntities db = new DBPetShopEntities();

        // GET: Admin/Species
        public ActionResult Index()
        {
            return View(db.Species.ToList());
        }

        // GET: Admin/Species/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // GET: Admin/Species/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSpecies,SpeciesName,Image")] Species species, HttpPostedFileBase fileimg)
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
                species.Image = fileimg.FileName;
                db.Species.Add(species);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(species);
        }

        // GET: Admin/Species/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // POST: Admin/Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSpecies,SpeciesName,Image")] Species species, HttpPostedFileBase fileimg)
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
                species.Image = fileimg.FileName;
                db.Entry(species).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(species);
        }

        // GET: Admin/Species/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // POST: Admin/Species/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Species species = db.Species.Find(id);
            db.Species.Remove(species);
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
