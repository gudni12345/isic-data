using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;

namespace ISIC_DATA.Controllers
{
    public class RegisterDogController : Controller
    {
        private DataAccess.DogContext db = new DataAccess.DogContext();

        [Authorize(Roles = "Administrator,SuperAdministrator")]                            // User must have the Administrator Role
        public ActionResult Index()
        {
            var viewModel = new DogViewModel();
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            ViewBag.successMessage = "";

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Index(DogViewModel viewModel)
        {   
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            ViewBag.successMessage = "";

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            if (!ModelState.IsValid)
                return View(viewModel);

            if (ModelState.IsValid)
            {
                try
                {

                    int uId = (int)WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);               
                    Litter l = new Litter()
                    {
                        DateOfBirth = viewModel.Litter.DateOfBirth,
                        FatherId = viewModel.Litter.FatherId,
                        MotherId = viewModel.Litter.MotherId,
                        PersonId = viewModel.Litter.PersonId,    // Breeder person.
                        UsersId =  uId                          // User logged in
                    };

                    db.Litter.Add(l);
                    db.SaveChanges();                           // Litter saved

                    foreach (DogAndPerson dp in viewModel.DogAndPersons)
                    {
                        Person P = new Person()
                        {
                            Name = dp.Person.Name,
                            Address = dp.Person.Address,
                            Email = dp.Person.Email,
                        };

                        db.Person.Add(P);               // Person saved. Owner
                        db.SaveChanges();

                        Users u = db.Users.Find(uId);

                        Dog d = new Dog()
                        {
                            Name = dp.Dog.Name,
                            Reg = dp.Dog.Reg,
                            Sex = dp.Dog.Sex,
                            Color = dp.Dog.Color,
                            LitterId = l.Id,             // Dog linked with privious litter.
                            PersonId = P.Id,           // Dog linked with Owner/Person    
                            BornInCountryId = u.CountryId // Dog gets same Country as the Admin.
                        };
                        if (d.Sex.Equals("Male")) d.Sex = "M";
                        if (d.Sex.Equals("Female")) d.Sex = "F";

                        db.Dog.Add(d);
                        db.SaveChanges();   // Dog saved
                    }    //end foreach DogAndPerson
                
                    TempData["Success"] = "Data was successfully saved.";
                } //end try
                catch (Exception)
                {                 
                    ViewData["Error"] = "Unable to save";
                    return RedirectToAction("Error");
                }
                
                return RedirectToAction("Index","Dog");  // Success
            } //end if Model state is valid

            return RedirectToAction("Index"); 
        }


        public JsonResult FathersJson()
        {
            return this.Json( new {
                   Result =  (from obj in db.Dog.Where(d => d.Sex == "M").OrderBy(d => d.Reg) 
                              select new { Id = obj.Id, Reg = obj.Reg, Name = obj.Name }) }, JsonRequestBehavior.AllowGet );
        }


        public JsonResult FetchFathers(string query)                                            //     Get all posible Fathers to json, used for typeAhead
        {
            List<Dog> fatherList = db.Dog.Where(d => d.Sex == "M").OrderBy(d => d.Reg).ToList();               
             //   Where(d => d.Reg.Contains("IS0")).ToList();
             //.OrderBy(d => d.Reg).ToList();            
         
            var serialisedJson = from result in fatherList 
                select new
                {
                    Reg = result.Reg,
                    Name = result.Name,
                    Id = result.Id     
                };
            return Json(serialisedJson , JsonRequestBehavior.AllowGet); 
        }

        public JsonResult FetchMothers(string query)                                             //     Get all posible Mothers to json, used for typeAhead
        {                                                                                         // Ath að bæta við að eldri hundar en 15 ára komi ekki fram
            List<Dog> motherList = db.Dog.Where(d => d.Sex == "F").OrderBy(d => d.Reg).ToList();

            var serialisedJson = from result in motherList
                                 select new
                                 {
                                     Reg = result.Reg,
                                     Name = result.Name,
                                     Id = result.Id
                                 };
            return Json(serialisedJson, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FetchBreeders(string q)                                   //     Get all posible Breeders to json, used for typeAhead
        {
            List<Person> breederList = db.Person.Where(p => p.Breeder == true).Where(p => p.Name.ToLower().StartsWith(q.ToLower())).ToList();
            var serialisedJson = from result in breederList
                                 select new { Name = result.Name, Id = result.Id };

            return Json(serialisedJson, JsonRequestBehavior.AllowGet);
        }




    }
}
