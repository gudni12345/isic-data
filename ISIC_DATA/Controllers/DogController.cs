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
using PagedList.Mvc;
using System.IO;
using System.Text.RegularExpressions;


namespace ISIC_DATA.Controllers
{
    public class DogController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Dog/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? CountryId)
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
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");

            var dogs = db.Dog.Include(d => d.Color).Include(d => d.Person).Include(d => d.BornInCountry).Include(d => d.Litter);

            if (CountryId != null)
            {// If country is selected                
                dogs = dogs.Where(d => d.BornInCountryId == CountryId);
                ViewBag.currentCountry = CountryId;
            }
            else
                ViewBag.currentCountry = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                dogs = dogs.Where(d => d.Name.ToUpper().Contains(searchString.ToUpper())
                                      || d.Reg.ToUpper().Contains(searchString.ToUpper()));
            }
            
            ViewBag.successMessage = "";
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



            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            
            ViewBag.numberOfDogsSelected = dogs.Count();

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
            else
            if (dog.LitterId != 1) 
            if ((dog.Litter.Father.LitterId != 1 ) || (dog.Litter.Mother.LitterId != 1))
            {

                // get siblings from the same litter // find all dogs that have same litterId as dog selected.
                ViewBag.Siblings = db.Dog.Where(d => d.LitterId == dog.LitterId).Where(d => d.Id != dog.Id).ToList();    
                
                // Find all dogs that have the same father
                ViewBag.SiblingsFromFatherSide = db.Dog.Where(d => d.Litter.FatherId == dog.Litter.FatherId)
                                                  .Where(d => d.Id != dog.Id).ToList();
                // Find all dogs that have the same mother
                ViewBag.SiblingsFromMotherSide = db.Dog.Where(d => d.Litter.MotherId == dog.Litter.MotherId)                                          
                                                .Where(d => d.Id != dog.Id).ToList();


                 //Find all puppies
                if (dog.Sex.Equals("M"))  // if Dog is male
                    ViewBag.Puppies = db.Dog.Where(d => d.Litter.FatherId == dog.Id).ToList();
                else
                    if (dog.Sex.Equals("F"))
                        ViewBag.Puppies = db.Dog.Where(d => d.Litter.MotherId == dog.Id).ToList();


            }
            return View(dog);
        }

 
        //
        // GET: /Dog/Create

        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create()
        {
            ViewBag.LitterId = new SelectList(db.Litter, "Id", "Id");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name");
            ViewBag.BornInCountryId = new SelectList(db.Country, "Id", "Name");
            ViewBag.LivesInCountryId = new SelectList(db.Country, "Id", "Name");
            return View();
        }

        //
        // POST: /Dog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create(Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Dog.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LitterId = new SelectList(db.Litter, "Id", "Id", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.BornInCountryId = new SelectList(db.Country, "Id", "Name", dog.BornInCountryId);
            ViewBag.LivesInCountryId = new SelectList(db.Country, "Id", "Name", dog.LivesInCountryId);
            return View(dog);
        }

        //
        // GET: /Dog/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(int id = 0)
        {
            Dog dog = db.Dog.Find(id);
            
            if (dog == null)
            {
                return HttpNotFound();
            }
          //  ViewBag.LitterId = new SelectList(db.Litter, "Id", "Id", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", dog.BornInCountryId);
            return View(dog);
        }

        //
        // POST: /Dog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Edit(Dog dog)
        {
     
            if (ModelState.IsValid)
            {
                string fileName=null;
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        // var fileName = Path.GetFileName(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        fileName = Regex.Replace(dog.Reg, @"[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;><!@#&\-\+/]", "");
                        fileName = fileName + extension;
                        var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                        file.SaveAs(path);
                    }
                }

                if (fileName != null)
                    dog.PicturePath = "~/Photos/"+fileName;
                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         //   ViewBag.LitterId = new SelectList(db.Litter, "Id", "Id", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", dog.BornInCountryId);



            return View(dog);
        }

        //
        // GET: /Dog/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult DeleteConfirmed(int id)
        {
            Dog dog = db.Dog.Find(id);
            db.Dog.Remove(dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadDocument");
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}