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
        private allISIC_DATARepository _db = null;

        public IsicDataController()
        {
            _db = new ISIC_DATARepository();
        }
        public IsicDataController(allISIC_DATARepository rep)
        {
            _db = rep;

        } 
        //
        // GET: /IsicData/

        public ActionResult IsicDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
        public ActionResult ISDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "IS"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }

        //Sækir gögn fyrir Damnörk
        public ActionResult DKDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                        where g.B == "DK"
                        select g;
            if ( isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }

        public ActionResult SEDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "SE"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }

        public ActionResult NODogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "NO"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
        
        public ActionResult FIDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "FI"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
        
        public ActionResult DEDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "DE"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
        public ActionResult NEDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "NE"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
        public ActionResult USDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "US"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
       
        public ActionResult AUDogs()
        {
            var isdogs = from g in _db.GetIsicDogs()
                         where g.B == "AU"
                         select g;
            if (isdogs != null)
            {
                return View(isdogs);
            }
            return View();
        }
    }
}
