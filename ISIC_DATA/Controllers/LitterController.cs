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
            var litter = db.Litter.Include(l => l.Breeder).Include(l => l.Father_Id).Include(l => l.Mother_Id);
            return View(litter.Take(20).ToList());
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
  //          ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id");
            ViewBag.DogName = new SelectList(db.Dog, "Name", "Name");
            return View();
        }

        //
        // POST: /Litter/Create

        [HttpPost]
        public ActionResult Create(Litter litter)
        {
            if (ModelState.IsValid)
            {
                db.Litter.Add(litter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.BreederId = new SelectList(db.Breeder, "Id", "Id", litter.BreederId);
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