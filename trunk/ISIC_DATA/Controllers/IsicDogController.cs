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
    public class IsicDogController : Controller
    {
        private IsicDogDBContext db = new IsicDogDBContext();

        //
        // GET: /IsicDog/

        public ActionResult Index()
        {
            return View(db.IsicDogs.ToList());
        }

        //
        // GET: /IsicDog/Details/5

        public ActionResult Details(int id = 0)
        {
            IsicDog isicdog = db.IsicDogs.Find(id);
            if (isicdog == null)
            {
                return HttpNotFound();
            }
            return View(isicdog);
        }

        //
        // GET: /IsicDog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /IsicDog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IsicDog isicdog)
        {
            if (ModelState.IsValid)
            {
                db.IsicDogs.Add(isicdog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(isicdog);
        }

        //
        // GET: /IsicDog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            IsicDog isicdog = db.IsicDogs.Find(id);
            if (isicdog == null)
            {
                return HttpNotFound();
            }
            return View(isicdog);
        }

        //
        // POST: /IsicDog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IsicDog isicdog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isicdog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(isicdog);
        }

        //
        // GET: /IsicDog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            IsicDog isicdog = db.IsicDogs.Find(id);
            if (isicdog == null)
            {
                return HttpNotFound();
            }
            return View(isicdog);
        }

        //
        // POST: /IsicDog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsicDog isicdog = db.IsicDogs.Find(id);
            db.IsicDogs.Remove(isicdog);
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