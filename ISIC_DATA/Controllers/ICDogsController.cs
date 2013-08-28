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
    public class ICDogsController : Controller
    {
        private ICDogDBContext db = new ICDogDBContext();

        //
        // GET: /ICDogs/

        public ActionResult Index()
        {
            return View(db.ICDogs.ToList());
        }

        //
        // GET: /ICDogs/Details/5

        public ActionResult Details(int id = 0)
        {
            ICDog icdog = db.ICDogs.Find(id);
            if (icdog == null)
            {
                return HttpNotFound();
            }
            return View(icdog);
        }

        //
        // GET: /ICDogs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ICDogs/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ICDog icdog)
        {
            if (ModelState.IsValid)
            {
                db.ICDogs.Add(icdog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(icdog);
        }

        //
        // GET: /ICDogs/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ICDog icdog = db.ICDogs.Find(id);
            if (icdog == null)
            {
                return HttpNotFound();
            }
            return View(icdog);
        }

        //
        // POST: /ICDogs/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ICDog icdog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(icdog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(icdog);
        }

        //
        // GET: /ICDogs/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ICDog icdog = db.ICDogs.Find(id);
            if (icdog == null)
            {
                return HttpNotFound();
            }
            return View(icdog);
        }

        //
        // POST: /ICDogs/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ICDog icdog = db.ICDogs.Find(id);
            db.ICDogs.Remove(icdog);
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