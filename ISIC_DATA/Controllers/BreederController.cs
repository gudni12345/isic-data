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
    public class BreederController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Breeder/

        public ActionResult Index()
        {
            var breeder = db.Breeder.Include(b => b.Person);
            return View(breeder.ToList());
        }

        //
        // GET: /Breeder/Details/5

        public ActionResult Details(int id = 0)
        {
            Breeder breeder = db.Breeder.Find(id);
            if (breeder == null)
            {
                return HttpNotFound();
            }
            return View(breeder);
        }

        //
        // GET: /Breeder/Create

        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name");
            return View();
        }

        //
        // POST: /Breeder/Create

        [HttpPost]
        public ActionResult Create(Breeder breeder)
        {
            if (ModelState.IsValid)
            {
                db.Breeder.Add(breeder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", breeder.PersonId);
            return View(breeder);
        }

        //
        // GET: /Breeder/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Breeder breeder = db.Breeder.Find(id);
            if (breeder == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", breeder.PersonId);
            return View(breeder);
        }

        //
        // POST: /Breeder/Edit/5

        [HttpPost]
        public ActionResult Edit(Breeder breeder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breeder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", breeder.PersonId);
            return View(breeder);
        }

        //
        // GET: /Breeder/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Breeder breeder = db.Breeder.Find(id);
            if (breeder == null)
            {
                return HttpNotFound();
            }
            return View(breeder);
        }

        //
        // POST: /Breeder/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Breeder breeder = db.Breeder.Find(id);
            db.Breeder.Remove(breeder);
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