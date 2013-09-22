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

            if (!ModelState.IsValid)
                return View(viewModel);

            if (ModelState.IsValid)
            {
                db.Litter.Add(viewModel.Litter);
                db.SaveChanges();

                foreach (Dog d in viewModel.Dogs)
                {
                    d.LitterId = db.Litter.Count();
                    db.Dog.Add(d);
                    db.SaveChanges();
                }

                
                return RedirectToAction("../Home");  // Success
            }

            return RedirectToAction("Index");
        }

    }
}
