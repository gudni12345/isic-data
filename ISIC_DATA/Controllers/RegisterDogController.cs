﻿using System;
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

       //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var viewModel = new DogViewModel();

            var Fathers = db.Dog.OrderBy(d => d.Reg).Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.OrderBy(d => d.Reg).Where(d => d.Sex == "F").ToList();
            ViewBag.MotherReg = new SelectList(Mothers, "Id", "Reg");
            ViewBag.FatherReg = new SelectList(Fathers, "Id", "Reg");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(DogViewModel viewModel)
        {
            var Fathers = db.Dog.OrderBy(d => d.Reg).Where(d => d.Sex == "M").ToList();
            var Mothers = db.Dog.OrderBy(d => d.Reg).Where(d => d.Sex == "F").ToList();
            ViewBag.MotherReg = new SelectList(Mothers, "Id", "Reg");
            ViewBag.FatherReg = new SelectList(Fathers, "Id", "Reg");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");

            List<string> genderList = new List<string>() { "Male", "Female" };
            ViewBag.Gender = new SelectList(genderList);

            if (!ModelState.IsValid)
                return View(viewModel);

            if (ModelState.IsValid)
            {
                Litter l = new Litter()
                {
                    DateOfBirth = viewModel.Litter.DateOfBirth,
                    FatherId = viewModel.Litter.FatherId,
                    MotherId = viewModel.Litter.MotherId
                };

                db.Litter.Add(l);
                db.SaveChanges();

                foreach (DogAndPerson dp in viewModel.DogAndPersons)
                {
                    Person P = new Person()
                    {
                        Name = dp.Person.Name,
                        Address = dp.Person.Address,
                        Email = dp.Person.Email,
                    };

                    db.Person.Add(P);
                    db.SaveChanges(); 

                    Dog d = new Dog()
                    {
                        Name = dp.Dog.Name,
                        Reg = dp.Dog.Reg,
                        Sex = dp.Dog.Sex,
                        Color = dp.Dog.Color,
                        LitterId = l.Id,             // Dog linked with privious litter.
                        PersonId = P.Id              // Dog linked with Owner/Person
                    };
                    if (d.Sex.Equals("Male")) d.Sex = "M";
                    if (d.Sex.Equals("Female")) d.Sex = "F";

                    db.Dog.Add(d);
                    db.SaveChanges();   // saving Dog to DB 
                }    //end foreach DogAndPerson
                return RedirectToAction("../Home");  // Success
            } //end if Model state is valid

            return RedirectToAction("Index");
        }


        public JsonResult FathersJson()
        {
            return this.Json( new {
                   Result =  (from obj in db.Dog.Where(d => d.Sex == "M").OrderBy(d => d.Reg) 
                              select new { Id = obj.Id, Reg = obj.Reg, Name = obj.Name }) }, JsonRequestBehavior.AllowGet );
        }


        public ActionResult AutoComplete(string term)
        {
            var Fathers = db
                .Dog
                .Where(d => d.Sex == "M")
                .OrderBy(d => d.Reg)
                .ToList();

            // .Select(d => d.Reg)
            return Json(Fathers, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FetchFathers(string query) // IN USE
        {
            List<Dog> fatherList = db.Dog.Where(d => d.Sex == "M").Where(d => d.Reg.Contains("IS0")).ToList();
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

        public ActionResult All()
        {
            return Json(db.Dog.ToList(), JsonRequestBehavior.AllowGet);
        }


    }
}
