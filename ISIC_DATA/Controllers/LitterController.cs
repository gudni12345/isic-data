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
    public class LitterController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Litter/
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
            var litters = db.Litter.Include(l => l.Father).Include(l => l.Mother);
            
            if (!String.IsNullOrEmpty(searchString))
            {
                litters = litters.Where(l => l.Father.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    litters = litters.OrderByDescending(l => l.Father.Name);
                    break;
                case "Date":
                    litters = litters.OrderBy(l => l.DateOfBirth);
                    break;
                default:
                    litters = litters.OrderByDescending(l => l.DateOfBirth);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(litters.ToPagedList(pageNumber, pageSize));

            // return View(dogs.Take(10));
           // return View(litter.Take(20).ToList());
            //return View();
        }

        //h
        // GET: /Litter/Details/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Details(int id = 0)
        {
            Litter litter = db.Litter.Find(id);
            if (litter == null)
            {
                return View("Error");
            }
            return View(litter);
        }

        //
        // GET: /Litter/Create
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create()
        {

            ViewBag.DogName = new SelectList(db.Dog, "Name", "Name");

            var Fathers = db.Dog.Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.Where(d => d.Sex == "F").ToList();
            ViewBag.MotherId = new SelectList(Mothers, "Id", "Name");
            ViewBag.FatherId = new SelectList(Fathers, "Id", "Name");

            return View();
        }

        //
        // POST: /Litter/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create(Litter litter)
        {
            if (ModelState.IsValid)
            {
                db.Litter.Add(litter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(litter);
        }

        //
        // GET: /Litter/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(int id = 0)
        {
            Litter litter = db.Litter.Find(id);
            if (litter == null)
            {
                return HttpNotFound();
            }
            
            return View(litter);
        }

        //
        // POST: /Litter/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(Litter litter)
        {
            if (ModelState.IsValid)
            {
                litter.Person = null; // No need to save any data to person table.  personId will be saved to litter.
                int uId = (int)WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);
                litter.UsersId = uId;                               // Saving User that changed the litter.

                db.Entry(litter).State = EntityState.Modified;
                //db.UpdateLitter(litter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(litter);
        }

        //
        // GET: /Litter/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
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
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
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

        public JsonResult FetchBreeders(string q)                                   //     Get all posible Breeders to json, used for typeAhead
        {
            //  List<Person> breederList = db.Person.Where(p => p.Breeder == true).Where(p => p.Name.ToLower().StartsWith(q.ToLower())).ToList();
            List<Person> breederList = db.Person.Where(p => p.Name.ToLower().StartsWith(q.ToLower())).ToList();
            var serialisedJson = from result in breederList
                                 select new { Name = result.Name, Id = result.Id };

            return Json(serialisedJson, JsonRequestBehavior.AllowGet);
        }
    }

    
}