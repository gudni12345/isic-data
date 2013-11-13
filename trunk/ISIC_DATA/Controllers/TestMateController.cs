using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;
using System.Diagnostics;

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

            double result = 0.0;   // A: Father, B: Mother

            // Check for no 1. Father/daughter, mother/son or brother/sister → 25% (1⁄4) 
            //***Pathmothod: n=2****
            if        (A.Id == B.Litter.FatherId) {result = 0.25;} // Father/daughter
            if   (A.Litter.MotherId == B.Id) {result += 0.25;}// son /mother

            // ***(n=3)***
            if   (A.Litter.FatherId == B.Litter.FatherId) {result += 0.125;} // A and B have same father 
            if   (A.Litter.MotherId == B.Litter.MotherId) {result += 0.125;} // A and B have same mother
            
            // Check for no 2.  Grandfather/granddaughter or grandmother/grandson → 12.5% (1⁄8)
            if  (A.Id == B.Litter.Father.Litter.FatherId) {result += 0.125;} // A is B´s Grandfather (farfar)
            if  (A.Id == B.Litter.Mother.Litter.FatherId) {result += 0.125;} // A is B´s Grandfather (morfar)
            if  (A.Litter.Mother.Litter.MotherId == B.Id) {result += 0.125;} // B is A´s Grandmother (mormor)
            if  (A.Litter.Father.Litter.MotherId == B.Id) {result += 0.125;} // B is A´s Grandmother (farmor)
            
            
                
             // Check for no 4.  Uncle/niece or aunt/nephew → 12.5% (1⁄8), if full related, half is 6.25%
             // ***(n=4)***
             if (A.Litter.FatherId == B.Litter.Father.Litter.FatherId) {result += 0.0625;} //A´s Father is B´s Grandpa (farfar)
             if (A.Litter.MotherId == B.Litter.Father.Litter.MotherId) {result += 0.0625;} //A´s Mother is B´s Grandmother (farmor)
             if (A.Litter.FatherId == B.Litter.Mother.Litter.FatherId) {result += 0.0625;} //A´s Father is B´s Grandpa (morfar)
             if (A.Litter.MotherId == B.Litter.Mother.Litter.MotherId) {result += 0.0625;} //A´s Mother is B´s Grandmother (farmor)
             if (A.Litter.Father.Litter.FatherId == B.Litter.FatherId) {result += 0.0625;} //B´s Father is A´s Grandpa (farfar)
             if (A.Litter.Father.Litter.MotherId == B.Litter.MotherId) {result += 0.0625;} //B´s Mother is A´s Grandmother (farmor)
             if (A.Litter.Mother.Litter.FatherId == B.Litter.FatherId) {result += 0.0625;} //B´s Father is A´s Grandpa (morfar)
             if (A.Litter.Mother.Litter.MotherId == B.Litter.MotherId) {result += 0.0625;} //B´s Mother is A´s Grandmother (farmor)
                       
             // 5.  Great-grandfather/great-granddaughter or great-grandmother/great-grandson → 6.25% (1⁄16)
             if (A.Id == B.Litter.Father.Litter.Father.Litter.FatherId) {result += 0.0625;} //A is B´s GreatGrandpa (either mom´s
             if (A.Id == B.Litter.Mother.Litter.Father.Litter.FatherId) {result += 0.0625;} //                or dad/granddad´s side)
             if (A.Id == B.Litter.Father.Litter.Mother.Litter.FatherId) {result += 0.0625;}
             if (A.Id == B.Litter.Mother.Litter.Mother.Litter.FatherId) {result += 0.0625;}
                 
             if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Id) {result += 0.0625;} //B is A´s GreatGrandmother (either mom´s
             if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Id) {result += 0.0625;} //                or dad´s side)
             if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Id) {result += 0.0625;}
             if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Id) {result += 0.0625;}
                          
             // 6.  Half-uncle/niece or half-aunt/nephew → 6.25% (1⁄16) --> covered under nr.4
             
             // 7.  First cousins → 6.25% (1⁄16) - Syskina börn - sami afi og amma, if two lines under 8. are true
             // 8.  Half First cousins → 3.125% (1⁄32) - Either grandma og grandpa the same
             // ***(n=5)***                             
             if (A.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.FatherId) {result += 0.03125;}  //  Same grandfather                
             if (A.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.FatherId) {result += 0.03125;}
             if (A.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.FatherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.FatherId) {result += 0.03125;}

             if (A.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.MotherId) {result += 0.03125;}  //  Same grandmother
             if (A.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.MotherId) {result += 0.03125;}
             if (A.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.MotherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.MotherId) {result += 0.03125;}
                                        

             // 9.  → 6,25% (1⁄32) - Great Grandma is the mother (Great-uncle/ neice and Great-aunt/ uncle)
             if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.MotherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.MotherId) {result += 0.03125;}
             if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.MotherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.MotherId) {result += 0.03125;}

             if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.FatherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.FatherId) {result += 0.03125;}
             if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.FatherId) {result += 0.03125;}
             if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.FatherId) {result += 0.03125;}

             if (B.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.MotherId) {result += 0.03125;}
             if (B.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.MotherId) {result += 0.03125;}
             if (B.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.MotherId) {result += 0.03125;}
             if (B.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.MotherId) {result += 0.03125;}

             if (B.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.FatherId) {result += 0.03125;}
             if (B.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.FatherId) {result += 0.03125;}
             if (B.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.FatherId) {result += 0.03125;}
             if (B.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.FatherId) {result += 0.03125;}

             //10. to be completed n=6



             
             result = result*100;
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
