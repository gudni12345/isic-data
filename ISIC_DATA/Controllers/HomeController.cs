using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;
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

        public ActionResult Contact()
        {
           
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Users()
        {

            return View(db.UserProfiles.ToList());
        }
    }
}
