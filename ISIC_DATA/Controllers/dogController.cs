using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;

namespace ISIC_DATA.Controllers
{
    public class dogController : Controller
    {
        private DogDBContext db = new DogDBContext();

        //
        // GET: /dog/

        public ActionResult Index()
        {
            return View(db.Dogs.ToList());
        }

        //
        // GET: /dog/Details/5

        public ActionResult Details(int id = 0)
        {
            dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // GET: /dog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /dog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Dogs.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dog);
        }

        //
        // GET: /dog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // POST: /dog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog);
        }

        //
        // GET: /dog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // POST: /dog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dog dog = db.Dogs.Find(id);
            db.Dogs.Remove(dog);
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