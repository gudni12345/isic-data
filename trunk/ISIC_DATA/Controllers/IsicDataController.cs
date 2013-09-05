using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;

namespace ISIC_DATA.Controllers
{
    public class IsicDataController : Controller
    {
        private ISIC_DATAEntities db = new ISIC_DATAEntities();
        private allISIC_DATARepository m_repository = null;

        public IsicDataController()
        {
            m_repository = new ISIC_DATARepository();
        }
        public IsicDataController(allISIC_DATARepository rep)
        {
            m_repository = rep;

        } 
        //
        // GET: /IsicData/

        public ActionResult IsicDogs()
        {
            var isicdogs = (from  g in m_repository.GetIsicDogs()
                            
                            select g).Take(10);

               return View();
             }

       

    }
}
