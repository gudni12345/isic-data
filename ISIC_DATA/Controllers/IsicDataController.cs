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
            var isicdogs = (from g in m_repository.GetIsicDogs()
                            
                            select g).Take(10);

               return View(isicdogs);
             }

           /* var activeGames = from games in m_repository.GetIsicDogs()

                              select games;


            return View(activeGames);
        }*/


       

                public ActionResult ISDogs()
                {
                    var isdogs = from g in m_repository.GetIsicDogs()
                                 where g.B == "IS"
                                 select g;
           
                    return View();
                }
        /*     
                     //Sækir gögn fyrir Damnörk
                     public ActionResult DKDogs()
                     {
                         var dkdogs = from g in m_repository.GetIsicDogs()
                                     where g.B == "DK"
                                     select g;
                         if ( dkdogs != null)
                         {
                             return View(dkdogs);
                         }
                         return View();
                     }
       
                            public ActionResult SEDogs()
                            {
                                var sedogs = from g in _db.GetIsicDogs()
                                             where g.B == "SE"
                                             select g;
                                if (sedogs != null)
                                {
                                    return View(sedogs);
                                }
                                return View();
                            }

                            public ActionResult NODogs()
                            {
                                var nodogs = from g in _db.GetIsicDogs()
                                             where g.B == "NO"
                                             select g;
                                if (nodogs != null)
                                {
                                    return View(nodogs);
                                }
                                return View();
                            }
        
                            public ActionResult FIDogs()
                            {
                                var fidogs = from g in _db.GetIsicDogs()
                                             where g.B == "FI"
                                             select g;
                                if (fidogs != null)
                                {
                                    return View(fidogs);
                                }
                                return View();
                            }
        
                            public ActionResult DEDogs()
                            {
                                var dedogs = from g in _db.GetIsicDogs()
                                             where g.B == "DE"
                                             select g;
                                if (dedogs != null)
                                {
                                    return View(dedogs);
                                }
                                return View();
                            }
                            public ActionResult NEDogs()
                            {
                                var nedogs = from g in _db.GetIsicDogs()
                                             where g.B == "NE"
                                             select g;
                                if (nedogs != null)
                                {
                                    return View(nedogs);
                                }
                                return View();
                            }
                            public ActionResult USDogs()
                            {
                                var usdogs = from g in _db.GetIsicDogs()
                                             where g.B == "US"
                                             select g;
                                if (usdogs != null)
                                {
                                    return View(usdogs);
                                }
                                return View();
                            }
       
                            public ActionResult AUDogs()
                            {
                                var usdogs = from g in _db.GetIsicDogs()
                                             where g.B == "AU"
                                             select g;
                                if (usdogs != null)
                                {
                                    return View(usdogs);
                                }
                                return View();
                            }*/
    }
}
