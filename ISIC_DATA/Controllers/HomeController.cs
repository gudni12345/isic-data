using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;
using PagedList;

namespace ISIC_DATA.Controllers
{
    public class HomeController : Controller
    {
        
        private DogContext db = new DogContext();

        public ActionResult Index()
        {
            ViewBag.numberOfDogs = db.Dog.Count();
            ViewBag.numberOfDogsIceland = db.Dog.Where(m => m.BornInCountryId == 1).ToList().Count;
            ViewBag.numberOfDogsGermany = db.Dog.Where(m => m.BornInCountryId == 2).ToList().Count;
            ViewBag.numberOfDogsHolland = db.Dog.Where(m => m.BornInCountryId == 3).ToList().Count;
            ViewBag.numberOfDogsUSA = db.Dog.Where(m => m.BornInCountryId == 4).ToList().Count;
            ViewBag.numberOfDogsFinland = db.Dog.Where(m => m.BornInCountryId == 5).ToList().Count;
            ViewBag.numberOfDogsNorway = db.Dog.Where(m => m.BornInCountryId == 6).ToList().Count;
            ViewBag.numberOfDogsSweden = db.Dog.Where(m => m.BornInCountryId == 7).ToList().Count;
            ViewBag.numberOfDogsDenmark = db.Dog.Where(m => m.BornInCountryId == 8).ToList().Count;
            ViewBag.numberOfDogsAustria = db.Dog.Where(m => m.BornInCountryId == 9).ToList().Count;
            ViewBag.numberOfDogsItaly = db.Dog.Where(m => m.BornInCountryId == 20).ToList().Count;
            ViewBag.numberOfDogsFrance = db.Dog.Where(m => m.BornInCountryId == 19).ToList().Count;
            ViewBag.numberOfDogsPoland = db.Dog.Where(m => m.BornInCountryId == 21).ToList().Count;
            ViewBag.numberOfDogsSwitzerland = db.Dog.Where(m => m.BornInCountryId == 10).ToList().Count;
            ViewBag.numberOfDogsBelgium = db.Dog.Where(m => m.BornInCountryId == 18).ToList().Count;
            ViewBag.numberOfDogsCanada= db.Dog.Where(m => m.BornInCountryId == 12).ToList().Count;

            var dogs = db.Dog.OrderByDescending(d => d.Litter.DateOfBirth).Where(d => d.PicturePath != null);
            return View(dogs.Take(5));
        }

        public ActionResult About()
        {
            
            var aboutnewsarticle = from a in db.NewsArticle
                                   where a.Valid == true && a.CategoriesName =="About"
                                   select a;

            return View(aboutnewsarticle);
        }


        public ActionResult News(string sortOrder, int? page , int? id)
        {                                      

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            var usernewsarticles = db.NewsArticle.Include(p => p.Users);
            usernewsarticles = from p in db.NewsArticle //only show valid news in users News View
                               where p.Valid == true && p.CategoriesName =="News"
                               select p;

            switch (sortOrder)
            {               
                
                case "Date":
                    usernewsarticles = usernewsarticles.OrderBy(p => p.Date);
                    break;
                case "Date desc":
                    usernewsarticles = usernewsarticles.OrderByDescending(p => p.Date);
                    break;

               default:
                    usernewsarticles = usernewsarticles.OrderByDescending(p => p.Date);
                    break; 
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //return View(db.NewsArticle.ToList());
            return View(usernewsarticles.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Contact()
        {
            return View();
        }
                                      
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Users()
        {
                 return View(db.UserProfiles.ToList());
        }
    }
}
