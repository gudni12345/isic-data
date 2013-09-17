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
    public class LitterController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Litter/

        public ActionResult Index()
        {
            var litter = db.Litter.Include(l => l.Breeder).Include(l => l.Father).Include(l => l.Mother);
            return View(litter.Take(5).ToList());
        }

        //
        // GET: /Litter/Details/5

        public ActionResult Details(int id = 0)
        {
            Litter litter = db.Litter.Find(id);
            if (litter == null)
            {
                return HttpNotFound();
            }
            return View(litter);
        }

        //
        // GET: /Litter/Create

        public ActionResult Create()
        {
            var Fathers = db.Dog.Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.Where(d => d.Sex == "F").ToList();
            ViewBag.MotherId = new SelectList(Mothers, "Id", "Name");
            ViewBag.FatherId = new SelectList(Fathers, "Id", "Name");
            ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id");
            return View();
        }

        //
        // POST: /Litter/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Litter litter)
        {
            if (ModelState.IsValid)
            {
                db.Litter.Add(litter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id", litter.BreederId);
            return View(litter);
        }

        //
        // GET: /Litter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Litter litter = db.Litter.Find(id);
            if (litter == null)
            {
                return HttpNotFound();
            }
            ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id", litter.BreederId);
            return View(litter);
        }

        //
        // POST: /Litter/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Litter litter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(litter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id", litter.BreederId);
            return View(litter);
        }

        //
        // GET: /Litter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Litter litter = db.Litter.Find(id);
            if (litter == null)
            {
                return HttpNotFound();
            }
            return View(litter);
        }

        //
        // POST: /Litter/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Litter litter = db.Litter.Find(id);
            db.Litter.Remove(litter);
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