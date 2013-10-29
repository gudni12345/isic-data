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
            var dogs = db.Dog.Include(d => d.Color).Include(d => d.DetailedInfo).Include(d => d.Person).Include(d => d.BornInCountry).Include(d => d.Litter);
            if (!String.IsNullOrEmpty(searchString))
            {
                dogs = dogs.Where(d => d.Name.ToUpper().Contains(searchString.ToUpper())
                                      || d.Reg.ToUpper().Contains(searchString.ToUpper()));
            }
            ViewBag.ColorId = new SelectList(db.Color, "Id", "ColorText");
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


            ViewBag.numberOfDogs = db.Dog.Count();
            ViewBag.numberOfDogsIceland = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 1).ToList().Count;
            ViewBag.numberOfDogsGermany = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 2).ToList().Count;
            ViewBag.numberOfDogsHolland = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 3).ToList().Count;
            ViewBag.numberOfDogsUSA = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 4).ToList().Count;
            ViewBag.numberOfDogsFinland = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 5).ToList().Count;
            ViewBag.numberOfDogsNorway = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 6).ToList().Count;
            ViewBag.numberOfDogsSweden = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 7).ToList().Count;
            ViewBag.numberOfDogsDenmark = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 8).ToList().Count;
            ViewBag.numberOfDogsAustria = db.Dog.AsEnumerable().Where(m => m.BornInCountryId == 9).ToList().Count;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dogs.ToPagedList(pageNumber, pageSize));
            // return View(dogs.Take(10));



        }

        public ActionResult About()
        {
            
            return View();
        }


        public ActionResult News(string sortOrder, int? page , int? id)
        {                                      

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            var usernewsarticles = db.NewsArticle.Include(p => p.Users);
            usernewsarticles = from p in db.NewsArticle //only show valid news in users News View
                               where p.Valid == true
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
