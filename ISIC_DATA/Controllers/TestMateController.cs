using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;

namespace ISIC_DATA.Controllers
{
    public class TestMateController : Controller
    {
        private DataAccess.DogContext db = new DataAccess.DogContext();

        //
        // GET: /TestMate/

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Index()
        {



            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Index(TestMateViewModel viewModel)
        {
            /*Typical coancestries between relatives are as follows:
              1.  Father/daughter, mother/son or brother/sister → 25% (1⁄4)
              2.  Grandfather/granddaughter or grandmother/grandson → 12.5% (1⁄8)
              3.  Half-brother/half-sister, Double cousins → 12.5% (1⁄8)
              4.  Uncle/niece or aunt/nephew → 12.5% (1⁄8)
              5.  Great-grandfather/great-granddaughter or great-grandmother/great-grandson → 6.25% (1⁄16)
              6   Half-uncle/niece or half-aunt/nephew → 6.25% (1⁄16)
              7.  First cousins → 6.25% (1⁄16)
             */
            
            double result = 0.0;
 
            // We only get Id of the Dog backfrom the viewModel.
            Dog A = db.Dog.Find(viewModel.Father.Id);
            Dog B = db.Dog.Find(viewModel.Mother.Id);

            // Check for no 1. Father/daughter, mother/son or brother/sister → 25% (1⁄4)
            if ((A.Id == B.Litter.FatherId) || // Father/daughter
                (A.Litter.MotherId == B.Id) || // son /mother
                (A.LitterId == B.LitterId) ||  // Same litter
                ((A.Litter.FatherId == B.Litter.FatherId) && (A.Litter.MotherId == B.Litter.MotherId))) // or have same parents
            {
                result = 25;
            }
            else    // Check for no 2.  Grandfather/granddaughter or grandmother/grandson → 12.5% (1⁄8)
                if ((A.Id == B.Litter.Father.Litter.FatherId) ||   //Grandfather/granddaughter
                    (A.Litter.Mother.Litter.MotherId == B.Id))      //grandmother/grandson
                {
                    result = 12.5;
                }
                else //Check for no 3.  Half-brother/half-sister, Double cousins → 12.5% (1⁄8)
                    if ((A.Litter.FatherId == B.Litter.FatherId) || // Same father or same Mother
                        (A.Litter.MotherId == B.Litter.MotherId))
                    {
                        result = 12.5;
                    }
                    else  // Check for no 4.  Uncle/niece or aunt/nephew → 12.5% (1⁄8)   
                        if ((A.Litter.FatherId == B.Litter.Father.Litter.FatherId) ||    // Uncle/niece 
                            (A.Litter.FatherId == B.Litter.Mother.Litter.FatherId) ||    // Uncle/niece                             
                            (A.Litter.MotherId == B.Litter.Father.Litter.MotherId) ||    // Uncle/niece 
                            (A.Litter.MotherId == B.Litter.Mother.Litter.MotherId) ||    // Uncle/niece 
                            (A.Litter.Father.Litter.FatherId == B.Litter.FatherId) ||   // aunt/nephew
                            (A.Litter.Mother.Litter.FatherId == B.Litter.FatherId) ||   // aunt/nephew
                            (A.Litter.Father.Litter.MotherId == B.Litter.MotherId) ||   // aunt/nephew
                            (A.Litter.Mother.Litter.MotherId == B.Litter.MotherId))     // aunt/nephew
                        {
                            result = 12.5;
                        }
            
            /*
            if (Father.LitterId == Mother.LitterId)   // Systkini í sama goti.
            {
                result = 0.5;
            }
            else
                if ((Father.Litter.FatherId == Mother.Litter.FatherId) &&
                    (Father.Litter.MotherId == Mother.Litter.MotherId)) // Sömu foreldrar
                {
                    result = 0.5;
                }


                else
                    result = 0.0;
            */

            ViewBag.Result = result;
            viewModel.Father.Name = A.Name;
            viewModel.Mother.Name = B.Name;
            ModelState.Clear();
            return View(viewModel);
        }

    }
}
