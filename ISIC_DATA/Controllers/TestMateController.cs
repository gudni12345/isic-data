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

        public double Inbreeding(Dog A, Dog B)
        {
            /*Typical coancestries between relatives are as follows:
                  1.  Father/daughter, mother/son or brother/sister → 25% (1⁄4)
                  2.  Grandfather/granddaughter or grandmother/grandson → 12.5% (1⁄8)
                  3.  Half-brother/half-sister, Double cousins → 12.5% (1⁄8)
                  4.  Uncle/niece or aunt/nephew → 12.5% (1⁄8)
                  5.  Great-grandfather/great-granddaughter or great-grandmother/great-grandson → 6.25% (1⁄16)
                  6.  Half-uncle/niece or half-aunt/nephew → 6.25% (1⁄16)
                  7.  First cousins → 6.25% (1⁄16)
                 */

            double result = 0.0;

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
                        if (((A.Litter.FatherId == B.Litter.Father.Litter.FatherId) &&    // Uncle/niece
                             (A.Litter.MotherId == B.Litter.Father.Litter.MotherId)) ||
                             ((A.Litter.FatherId == B.Litter.Mother.Litter.FatherId) &&  // Uncle/niece
                              (A.Litter.MotherId == B.Litter.Mother.Litter.MotherId)) ||

                            ((A.Litter.Father.Litter.FatherId == B.Litter.FatherId) &&   // aunt/nephew
                            (A.Litter.Father.Litter.MotherId == B.Litter.MotherId)) ||
                            ((A.Litter.Mother.Litter.FatherId == B.Litter.FatherId) &&   // aunt/nephew  
                            (A.Litter.Mother.Litter.MotherId == B.Litter.MotherId)))
                        {
                            result = 12.5;
                        }
                        else // 5.  Great-grandfather/great-granddaughter or great-grandmother/great-grandson → 6.25% (1⁄16)
                            if ((A.Id == B.Litter.Father.Litter.Father.Litter.FatherId) || // Great-grandfather/great-granddaughter
                                (A.Id == B.Litter.Mother.Litter.Father.Litter.FatherId) ||
                                (A.Id == B.Litter.Father.Litter.Mother.Litter.FatherId) ||
                                (A.Id == B.Litter.Mother.Litter.Mother.Litter.FatherId) ||

                                (A.Litter.Father.Litter.Father.Litter.FatherId == B.Id) ||  // great-grandmother/great-grandson
                                (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Id) ||
                                (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Id) ||
                                (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Id))
                            {
                                result = 6.25;
                            }
                            else // 6.  Half-uncle/niece or half-aunt/nephew → 6.25% (1⁄16)
                                if ((A.Litter.FatherId == B.Litter.Father.Litter.FatherId) ||    // Uncle/niece
                                     (A.Litter.MotherId == B.Litter.Father.Litter.MotherId) ||
                                     (A.Litter.FatherId == B.Litter.Mother.Litter.FatherId) ||  // Uncle/niece
                                     (A.Litter.MotherId == B.Litter.Mother.Litter.MotherId) ||

                                     (A.Litter.Father.Litter.FatherId == B.Litter.FatherId) ||   // aunt/nephew
                                     (A.Litter.Father.Litter.MotherId == B.Litter.MotherId) ||
                                     (A.Litter.Mother.Litter.FatherId == B.Litter.FatherId) ||   // aunt/nephew  
                                     (A.Litter.Mother.Litter.MotherId == B.Litter.MotherId))
                                {
                                    result = 6.25;
                                }
                                else // 7.  First cousins → 6.25% (1⁄16) - Syskina börn - sami afi og amma
                                    if (((A.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.FatherId) ||  //  Same grandfather
                                          (A.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.FatherId) ||
                                          (A.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.FatherId) ||
                                          (A.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.FatherId)) &&

                                          ((A.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.MotherId) ||  //  Same grandmother
                                          (A.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.MotherId) ||
                                          (A.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.MotherId) ||
                                          (A.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.MotherId)))
                                    {
                                        result = 6.25;
                                    }

            return result;
        }



        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Index(TestMateViewModel viewModel)
        {

            
            double vresult = 0.0;
 
            // We only get Id of the Dog backfrom the viewModel.
            Dog FatherA = db.Dog.Find(viewModel.Father.Id);
            Dog MotherB = db.Dog.Find(viewModel.Mother.Id);

            vresult = Inbreeding(FatherA, MotherB);


            ViewBag.Result = vresult;
            viewModel.Father.Name = FatherA.Name;
            viewModel.Mother.Name = MotherB.Name;
            ModelState.Clear();
            return View(viewModel);
        }

    }
}
