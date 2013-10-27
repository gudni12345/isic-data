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
           

            return View();
        }

        public ActionResult About()
        {
            
            return View();
        }


        public ActionResult News(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
           // ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            var usernewsarticles = db.NewsArticle.Include(p => p.Users);
            
           
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
