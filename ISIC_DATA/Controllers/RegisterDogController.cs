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
            if (viewModel.Litter.FatherId != 0)  // The name of the father is displayed in form. the following code is to clear false positive validation errors
            {
                Dog father = db.Dog.Find(viewModel.Litter.FatherId);
                viewModel.Litter.Father.Name = father.Name;
                viewModel.Litter.Father.Sex = "M";
                if (ModelState.ContainsKey("Litter.Father.Sex"))
                    ModelState["Litter.Father.Sex"].Errors.Clear();
                if (ModelState.ContainsKey("Litter.Father.Name"))
                    ModelState["Litter.Father.Name"].Errors.Clear();
            }

            if (viewModel.Litter.MotherId != 0)
            {
                Dog mother = db.Dog.Find(viewModel.Litter.MotherId);
                viewModel.Litter.Mother.Name = mother.Name;
                viewModel.Litter.Mother.Sex = "F";
                if (ModelState.ContainsKey("Litter.Mother.Sex"))
                    ModelState["Litter.Mother.Sex"].Errors.Clear();
                if (ModelState.ContainsKey("Litter.Mother.Name"))
                    ModelState["Litter.Mother.Name"].Errors.Clear();
            }

            if (viewModel.Litter.DateOfBirth == null)
            {
                ModelState.AddModelError("DateOfBirth", "Date is required.");
            }
            if (viewModel.Litter.PersonId == null)
            {
                ModelState.AddModelError("PersonId", "Breeder is required.");
            }           


            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
            ViewBag.successMessage = "";

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View(viewModel);
            }

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

                    foreach (Dog dp in viewModel.Dogs)
                    {                        

                        Users u = db.Users.Find(uId);  // Find Admin that is logged in.

                        Dog d = new Dog()
                        {
                            Name = dp.Name,
                            Reg = dp.Reg,
                            Sex = dp.Sex,
                            Color = dp.Color,
                            LitterId = l.Id,             // Dog linked with privious litter.                            
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




        public JsonResult FetchFathers(string q)                                            //     Get all posible Fathers to json, used for typeAhead
        {
            List<Dog> fatherList = db.Dog.Where(d => d.Sex == "M").Where(d => d.Reg.ToLower().StartsWith(q.ToLower())).ToList();
            var serialisedJson = from result in fatherList 
                select new
                {
                    Reg = result.Reg,
                    Name = result.Name,
                    Id = result.Id     
                };
            return Json(serialisedJson , JsonRequestBehavior.AllowGet); 
        }

        public JsonResult FetchMothers(string q)                                             //     Get all posible Mothers to json, used for typeAhead
        {                                                                                        
            List<Dog> motherList = db.Dog.Where(d => d.Sex == "F").Where(d => d.Reg.ToLower().StartsWith(q.ToLower())).ToList();
            var serialisedJson = from result in motherList
                                 select new { Reg = result.Reg, Name = result.Name, Id = result.Id };
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
