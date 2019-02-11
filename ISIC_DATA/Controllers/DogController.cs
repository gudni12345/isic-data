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
        // Notum repository. Þannig hægt sé að nota þennan controller einnig með fake repository. 
        // semsagt með prófunargögn fyrir unitTest.
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

        public ActionResult Info()
        {
            return PartialView("Info");
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
            ViewBag.Pedigree = FetchPedigree(dog);
            return View(dog);
        }

        // GET: /Dog/Pedigree/5

        public ActionResult Pedigree(int id = 0)
        {
           // ViewBag.Pedigree = FetchPedigree(id);
            return View();
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
            // The litter is displayed in form. the following code is to clear false positive validation errors
            if ((dog.LitterId != null) && (dog.Litter.PersonId == null))
            {
                if (ModelState.ContainsKey("Litter.PersonId"))
                    ModelState["Litter.PersonId"].Errors.Clear();
            }
          
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

        private List<Dog> parents(Dog d)  // Skilum foreldrum í lista.
        {
            List<Dog> pList = new List<Dog>();
            pList.Add(d.Litter.Father);
            pList.Add(d.Litter.Mother);
            return pList;
        }

        // Býr til list af foreldrum fyrir ættbók.                              
        public List<Dog> FetchPedigree(Dog dog)
        {
            List<Dog> parentList = new List<Dog>();
            List<Dog> pTemp = new List<Dog>();
            List<Dog> pTemp2 = new List<Dog>();
            List<Dog> pTemp3 = new List<Dog>();
            parentList = parents(dog);  // Skilar foreldrum.  2 hundar

            foreach (Dog d in parentList)
            {
                if (pTemp == null)
                    pTemp = parents(d);
                else
                    pTemp.AddRange(parents(d));        // 4 hundar.
            }

            parentList.AddRange(pTemp);  // 6 hundar komnir.

            foreach (Dog d in pTemp) // finnur foreldra fyrir þessa 4 hunda. 
            {
                if (pTemp2 == null)
                    pTemp2 = parents(d);
                else        
                pTemp2.AddRange(parents(d));        // skila 8 hundar.
            }
            parentList.AddRange(pTemp2);    // 6 + 8 = 14 stk
            pTemp = null;

            foreach (Dog d in pTemp2) // finnur foreldra fyrir þessa 8 hunda. 
            {
                if (pTemp == null)
                    pTemp = parents(d);
                else
                    pTemp.AddRange(parents(d));        //skila 16 hundar.
            }
            parentList.AddRange(pTemp);  // 14 + 16 = 30 stk

            pTemp2 = null;

            foreach (Dog d in pTemp) // finnur þetta foreldra fyrir 16 hundana ?
            {
                if (pTemp2 == null)
                    pTemp2 = parents(d);
                else
                    pTemp2.AddRange(parents(d));        // skila 32 hundar.
            }

            parentList.AddRange(pTemp2);  // 16 + 32 = 48 stk


            return parentList;
        }
    }
}