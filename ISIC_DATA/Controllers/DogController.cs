using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;

namespace ISIC_DATA.Controllers
{
    public class DogController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Dog/

        public ActionResult Index()
        {
            var dog = db.Dog.Include(d => d.Litter).Include(d => d.Color);
            return View(dog.ToList());
        }

        //
        // GET: /Dog/Details/5

        public ActionResult Details(string id = null)
        {
            Dog dog = db.Dog.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // GET: /Dog/Create

        public ActionResult Create()
        {
            ViewBag.LitterId = new SelectList(db.Litter, "LitterId", "Reg_Mother");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorFile");
            return View();
        }

        //
        // POST: /Dog/Create

        [HttpPost]
        public ActionResult Create(Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Dog.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LitterId = new SelectList(db.Litter, "LitterId", "Reg_Mother", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorFile", dog.ColorId);
            return View(dog);
        }

        //
        // GET: /Dog/Edit/5

        public ActionResult Edit(string id = null)
        {
            Dog dog = db.Dog.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            ViewBag.LitterId = new SelectList(db.Litter, "LitterId", "Reg_Mother", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorFile", dog.ColorId);
            return View(dog);
        }

        //
        // POST: /Dog/Edit/5

        [HttpPost]
        public ActionResult Edit(Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LitterId = new SelectList(db.Litter, "LitterId", "Reg_Mother", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorFile", dog.ColorId);
            return View(dog);
        }

        //
        // GET: /Dog/Delete/5

        public ActionResult Delete(string id = null)
        {
            Dog dog = db.Dog.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // POST: /Dog/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Dog dog = db.Dog.Find(id);
            db.Dog.Remove(dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}