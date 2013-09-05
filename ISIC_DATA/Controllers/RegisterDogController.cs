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
    public class RegisterDogController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /RegisterDog/

        public ActionResult Index()
        {
            return View(db.RegisterDogs.ToList());
        }

        //
        // GET: /RegisterDog/Details/5

        public ActionResult Details(string id = null)
        {
            RegisterDog registerdog = db.RegisterDogs.Find(id);
            if (registerdog == null)
            {
                return HttpNotFound();
            }
            return View(registerdog);
        }

        //
        // GET: /RegisterDog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RegisterDog/Create

        [HttpPost]
        public ActionResult Create(RegisterDog registerdog)
        {
            if (ModelState.IsValid)
            {
                db.RegisterDogs.Add(registerdog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerdog);
        }

        //
        // GET: /RegisterDog/Edit/5

        public ActionResult Edit(string id = null)
        {
            RegisterDog registerdog = db.RegisterDogs.Find(id);
            if (registerdog == null)
            {
                return HttpNotFound();
            }
            return View(registerdog);
        }

        //
        // POST: /RegisterDog/Edit/5

        [HttpPost]
        public ActionResult Edit(RegisterDog registerdog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerdog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerdog);
        }

        //
        // GET: /RegisterDog/Delete/5

        public ActionResult Delete(string id = null)
        {
            RegisterDog registerdog = db.RegisterDogs.Find(id);
            if (registerdog == null)
            {
                return HttpNotFound();
            }
            return View(registerdog);
        }

        //
        // POST: /RegisterDog/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            RegisterDog registerdog = db.RegisterDogs.Find(id);
            db.RegisterDogs.Remove(registerdog);
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