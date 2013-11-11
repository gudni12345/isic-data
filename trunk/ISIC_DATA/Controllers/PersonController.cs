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
    public class PersonController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Person/
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
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

            var persons = db.Person.Include(p => p.Country);
            
            if (!String.IsNullOrEmpty(searchString))
            {
                persons=persons.Where(p=> p.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_dec":
                    persons= persons.OrderByDescending(p=> p.Name);
                    break;

                case "country":
                    persons = persons.OrderBy(p => p.Country.Name);
                    break;
                default:
                    persons = persons.OrderByDescending (p=> p.Country.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(persons.ToPagedList(pageNumber, pageSize));
           // var person = db.Person.Include(p => p.Country);
           // return View(person.Take(20).ToList());
        }

        //
        // GET: /Person/Details/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Details(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return View("Error");
            }
            return View(person);
        }

        //
        // GET: /Person/Create
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            return View();
        }

        //
        // POST: /Person/Create

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", person.CountryId);
            return View(person);
        }


        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult CreateBreeder()
        {
            ViewBag.ReturnUrl = Request.UrlReferrer;
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            return PartialView("CreateBreeder");
        }

        //
        // POST: /Person/Create

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult CreateBreeder(Person person)
        {
            if (person.Name == null)
            {
                ModelState.AddModelError("Name", "Name is required.");
            }

            if (person.CountryId == null)
            {
                ModelState.AddModelError("CountryId", "Country is required.");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    person.Breeder = true;
                    db.Person.Add(person);
                    db.SaveChanges();
                    //return Json(new { Message = "success" });
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", person.CountryId);
            return PartialView("CreateBreeder", person);
        }


        // Create owner

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult CreateOwner()
        {
            ViewBag.ReturnUrl = Request.UrlReferrer;
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            return PartialView("CreateOwner");
        }

        //
        // POST: /Person/Create

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult CreateOwner(Person person)
        {
            if (person.Name == null)
            {
                ModelState.AddModelError("Name", "Name is required.");
            }

            if (person.CountryId == null)
            {
                ModelState.AddModelError("CountryId", "Country is required.");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    person.Owner = true;
                    db.Person.Add(person);
                    db.SaveChanges();
                    //return Json(new { Message = "success" });
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", person.CountryId);
            return PartialView("CreateOwner", person);
        }






        //
        // GET: /Person/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", person.CountryId);
            return View(person);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", person.CountryId);
            return View(person);
        }

        //
        // GET: /Person/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Delete(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Person/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
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