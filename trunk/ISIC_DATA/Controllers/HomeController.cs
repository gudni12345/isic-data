using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISIC_DATA.Controllers
{
    public class HomeController : Controller
    {
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
       /* public ActionResult Users()
        {
            
            var query = from u in n_repository.UserProfiles
                        where u.UserId > 0
                        orderby u.UserName ascending
                        select u;

            var userProfiles = m_repository.GetUserProfiles();
            return View(userProfiles);
        }*/
    }
}
