using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;
using PagedList;

namespace ISIC_DATA.Controllers
{
    public class DogController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Dog/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var dogs = db.Dog.Include(d => d.Color).Include(d => d.DetailedInfo).Include(d => d.Person).Include(d => d.Country).Include(d => d.Litter);
            if (!String.IsNullOrEmpty(searchString))
            {
                dogs = dogs.Where(d => d.Name.ToUpper().Contains(searchString.ToUpper()) );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    dogs = dogs.OrderByDescending(d => d.Name);
                    break;
                case "Date":
                    dogs = dogs.OrderBy(d => d.Litter.DateOfBirth);
                    break;
                default:
                    dogs = dogs.OrderByDescending(d => d.Litter.DateOfBirth);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dogs.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Dog/Details/5

        public ActionResult Details(int id = 0)
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
            ViewBag.LitterId = new SelectList(db.Litter, "Id", "Id");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            ViewBag.DetailedInfoId = new SelectList(db.DetailedInfo, "Id", "OldColor");
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Country, "Id", "CountryCode");
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

            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.DetailedInfoId = new SelectList(db.DetailedInfo, "Id", "OldColor", dog.DetailedInfoId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "CountryCode", dog.CountryId);
            return View(dog);
        }

        //
        // GET: /Dog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Dog dog = db.Dog.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
    
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.DetailedInfoId = new SelectList(db.DetailedInfo, "Id", "OldColor", dog.DetailedInfoId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "CountryCode", dog.CountryId);
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
          
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.DetailedInfoId = new SelectList(db.DetailedInfo, "Id", "OldColor", dog.DetailedInfoId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "CountryCode", dog.CountryId);
            return View(dog);
        }

        //
        // GET: /Dog/Delete/5

        public ActionResult Delete(int id = 0)
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
        public ActionResult DeleteConfirmed(int id)
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