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
    public class CountryController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Country/
        [Authorize(Roles = "Administrator,SuperAdministrator")]  
        public ActionResult Index()
        {
            return View(db.Country.ToList());
        }

        //
        // GET: /Country/Details/5
        [Authorize(Roles = "Administrator,SuperAdministrator")]     
        public ActionResult Details(int id = 0)
        {
            Country country = db.Country.Find(id);
            if (country == null)
            {
               return View( "Error" );
            }
            return View(country);
        }

        //
        // GET: /Country/Create
        [Authorize(Roles = "Administrator,SuperAdministrator")]             
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Country/Create

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]     
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Country.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        //
        // GET: /Country/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")]     
        public ActionResult Edit(int id = 0)
        {
            Country country = db.Country.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Country/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]     
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        //
        // GET: /Country/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")]  
        public ActionResult Delete(int id = 0)
        {
            Country country = db.Country.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Country/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]   
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Country.Find(id);
            db.Country.Remove(country);
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