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

    // Admin controller fyrir umsjónaraðila, helstu aðgerðir fyrir hann.

    public class AdminController : Controller
    {
        private DataAccess.DogContext db = new DataAccess.DogContext();
        //
        // GET: /Admin/

        [Authorize(Roles = "Administrator,SuperAdministrator")]   
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]   
        public ActionResult RegisterDog()
        {

            return View();
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]   
        public ActionResult EditNews()
        {

            return View();
        }

    }
}
