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

        public ActionResult Index()
        {
            var viewModel = new DogViewModel();

            var Fathers = db.Dog.Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.Where(d => d.Sex == "F").ToList();
            ViewBag.MotherId = new SelectList(Mothers, "Id", "Name");
            ViewBag.FatherId = new SelectList(Fathers, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(DogViewModel viewModel)
        {
            var Fathers = db.Dog.Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.Where(d => d.Sex == "F").ToList();
            ViewBag.MotherId = new SelectList(Mothers, "Id", "Name");
            ViewBag.FatherId = new SelectList(Fathers, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            if (!ModelState.IsValid)
                return View(viewModel);

            if (ModelState.IsValid)
            {
                db.Litter.Add(viewModel.Litter);
                db.SaveChanges();

                foreach (DogAndPerson dp in viewModel.DogAndPersons)
                {                    
                    if (dp.Dog.Sex.Equals("Male")) dp.Dog.Sex = "M";
                    if (dp.Dog.Sex.Equals("Female")) dp.Dog.Sex = "F";
                    dp.Dog.LitterId = db.Litter.Count();  // DOg linked to LitterId
                    dp.Dog.PersonId = db.Person.Count()+1;  // Dog linked to PersonId
                    db.Dog.Add(dp.Dog);
                    db.Person.Add(dp.Person);
                    db.SaveChanges();
                }

                
                return RedirectToAction("../Home");  // Success
            }

            return RedirectToAction("Index");
        }

    }
}
