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
using System.Web.Helpers;


namespace ISIC_DATA.Controllers
{
    public class DogController : Controller
    {
   //    private DogRepository db = new DogRepository();

        DogRepository db;

        public DogController()
            : this(new DogRepository()) {
        }

        public DogController(DogRepository repository) {
            db = repository;
        }

        

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
            ViewBag.CountryId = new SelectList(db.allCountries, "Id", "Name");

            var dogs = db.allDogs.Include(d => d.Color).Include(d => d.Person).Include(d => d.BornInCountry).Include(d => d.Litter);

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
                                      || d.Reg.ToUpper().Contains(searchString.ToUpper())
                                      || d.NewReg.ToUpper().Contains(searchString.ToUpper()));
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



            ViewBag.ColorId = new SelectList(db.allColors, "Id", "ColorText");
            
            ViewBag.numberOfDogsSelected = dogs.Count();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dogs.ToPagedList(pageNumber, pageSize));                                            
        }





        //
        // GET: /Dog/Details/5

        public ActionResult Details(int id = 0)
        {
            Dog dog = db.FindDog(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            else
            if (dog.LitterId != 1)
            {
                if ((dog.Litter.Father.LitterId != 1) || (dog.Litter.Mother.LitterId != 1))
                {

                    // get siblings from the same litter // find all dogs that have same litterId as dog selected.
                    ViewBag.Siblings = db.allDogs.Where(d => d.LitterId == dog.LitterId).Where(d => d.Id != dog.Id).ToList();

                    // Find all dogs that have the same father
                    ViewBag.SiblingsFromFatherSide = db.allDogs.Where(d => d.Litter.FatherId == dog.Litter.FatherId)
                                                      .Where(d => d.Id != dog.Id).ToList();
                    // Find all dogs that have the same mother
                    ViewBag.SiblingsFromMotherSide = db.allDogs.Where(d => d.Litter.MotherId == dog.Litter.MotherId)
                                                    .Where(d => d.Id != dog.Id).ToList();

                }
                 //Find all puppies
                if (dog.Sex.Equals("M"))  // if Dog is male
                    ViewBag.Puppies = db.allDogs.Where(d => d.Litter.FatherId == dog.Id).ToList();
                else
                    if (dog.Sex.Equals("F"))
                        ViewBag.Puppies = db.allDogs.Where(d => d.Litter.MotherId == dog.Id).ToList();
            

            }
            return View(dog);
        }

 
        //
        // GET: /Dog/Create

        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Create()
        {
            ViewBag.LitterId = new SelectList(db.allLitters, "Id", "Id");
            ViewBag.ColorId = new SelectList(db.allColors, "Id", "ColorText");
            ViewBag.PersonId = new SelectList(db.allPersons, "Id", "Name");
            ViewBag.BornInCountryId = new SelectList(db.allCountries, "Id", "Name");
            ViewBag.LivesInCountryId = new SelectList(db.allCountries, "Id", "Name");
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
                db.InsertOrUpdateDog(dog);
               // db.Dog.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LitterId = new SelectList(db.allLitters, "Id", "Id", dog.LitterId);
            ViewBag.ColorId = new SelectList(db.allColors, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.allPersons, "Id", "Name", dog.PersonId);
            ViewBag.BornInCountryId = new SelectList(db.allCountries, "Id", "Name", dog.BornInCountryId);
            ViewBag.LivesInCountryId = new SelectList(db.allCountries, "Id", "Name", dog.LivesInCountryId);
            return View(dog);
        }

        //
        // GET: /Dog/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Edit(int id = 0)
        {            
            Dog dog = db.FindDog(id);
            
            if (dog == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.ColorId = new SelectList(db.allColors, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.allPersons, "Id", "Name", dog.PersonId);

            if (dog.PersonId != null)
                ViewBag.Owner = dog.Person.Name;
            ViewBag.BornInCountryId = new SelectList(db.allCountries, "Id", "Name", dog.BornInCountryId);
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
                string fileName = null;
                dog.BornInCountry = null;      // Link to Country table dismissed in this change. the field is not changeable in the view.
                
                dog.Litter = null;

                try  // Try to save picture
                {                    
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            WebImage img = new WebImage(file.InputStream);   // Making sure the picture is of rigth size
                            if (img.Width > 500)
                                img.Resize(500, 315);

                            var extension = Path.GetExtension(file.FileName);
                            fileName = Regex.Replace(dog.Reg, @"[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;><!@#&\-\+/]", "");
                            fileName = fileName + extension;
                            var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                            
                            img.Save(path);
                            TempData["Success"] = "Picture for the " + dog.Name + " was successfully saved.";
                        }
                    }
                }
                catch (Exception)
                {
                    ViewData["Error"] = "Unable to save picture";
                }

                if (fileName != null)
                    dog.PicturePath = "~/Photos/"+fileName;
                db.UpdateDog(dog);
               // db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            ViewBag.ColorId = new SelectList(db.allColors, "Id", "ColorText", dog.ColorId);
            ViewBag.PersonId = new SelectList(db.allPersons, "Id", "Name", dog.PersonId);
            ViewBag.CountryId = new SelectList(db.allCountries, "Id", "Name", dog.BornInCountryId);

            return View(dog);
        }

        //
        // GET: /Dog/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Delete(int id = 0)
        {           
            Dog dog = db.FindDog(id);
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
         //   Dog dog = db.FindDog(id);
            db.DeleteDog(id);
            //db.Dog.Remove(dog);
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


        public JsonResult FetchOwners(string q)                                   //     Get all posible Owners to json, used for typeAhead
        {
            List<Person> ownerList = db.allPersons.Where(p => p.Name.ToLower().StartsWith(q.ToLower())).ToList();
            var serialisedJson = from result in ownerList
                                 select new { Name = result.Name, Id = result.Id };

            return Json(serialisedJson, JsonRequestBehavior.AllowGet);
        }


    }
}