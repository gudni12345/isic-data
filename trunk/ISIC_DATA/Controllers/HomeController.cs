using System;
using System.Collections.Generic;
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


        public ActionResult News()
        {
           
            return View(db.NewsArticle.ToList());
           
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
