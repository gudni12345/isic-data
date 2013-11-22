using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;
using System.Diagnostics;
using System.Data;
using System.Data.Entity;

namespace ISIC_DATA.Controllers
{
    public class TestMateController : Controller
    {
        private DataAccess.DogContext db = new DataAccess.DogContext();

        //
        // GET: /TestMate/

       
        public ActionResult Index()
        {
      
            return View();
        }


        [HttpPost]        
        public ActionResult Index(TestMateViewModel viewModel)
        {         
            double vresult = 0.0;

            // We only get Id of the Dog backfrom the viewModel.
            Dog FatherA = db.Dog.Find(viewModel.Father.Id);
            Dog MotherB = db.Dog.Find(viewModel.Mother.Id);

            viewModel.Father.LitterId = FatherA.LitterId;           //We add litter info to the ViewModel.
            viewModel.Mother.LitterId = MotherB.LitterId;
            viewModel.Father.Litter = FatherA.Litter;
            viewModel.Mother.Litter = MotherB.Litter;

            vresult = Inbreeding(FatherA, MotherB);

            ViewBag.Result = vresult;
            viewModel.Father.Name = FatherA.Name;
            viewModel.Mother.Name = MotherB.Name;
            ModelState.Clear();
            return View(viewModel);
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

              
                        
            //if father and mother have the same parents, the tree does not need to be checked further. No more possible path.
            if ((A.Litter.FatherId == B.Litter.FatherId) && (A.Litter.MotherId == B.Litter.MotherId)) { result += 0.25; }
            //To do: get mothers (10%) and fathers(5%) inbreeding and calculate: result = result*1.15

            //***Pathmothod: n=2****
            //Incest: because father or mother appears on both sides, we concentrate on the tree of the longer relation.
            //     It´s not the perfect calculation, but it gives a better result (**still in construction)
            // Father/daughter
            else if (A.Id == B.Litter.FatherId)
            {
                result = 0.25;
                if (B.Litter.Mother.Litter.FatherId == A.Id) { result += 0.125; } //check if Father is also Grandfather
                else if (B.Litter.Mother.Litter.Father.Litter.FatherId == A.Id) { result += 0.0625; } //check if Father is mothers Great Grandfather
                else if (B.Litter.Mother.Litter.Mother.Litter.FatherId == A.Id) { result += 0.0625; }
                else if (B.Litter.Mother.Litter.Father.Litter.Father.Litter.FatherId == A.Id) { result += 0.03125; } //check if Father is mothers Great Great Grandfather
                else if (B.Litter.Mother.Litter.Mother.Litter.Father.Litter.FatherId == A.Id) { result += 0.03125; }
                else if (B.Litter.Mother.Litter.Father.Litter.Mother.Litter.FatherId == A.Id) { result += 0.03125; }
                else if (B.Litter.Mother.Litter.Mother.Litter.Mother.Litter.FatherId == A.Id) { result += 0.03125; }
                //To do: get fathers inbreeding (F) and calculate: result = result * 1.F
            }
            //mother/son
            else if (B.Id == A.Litter.MotherId)
            {
                result = 0.25;
                if (A.Litter.Mother.Litter.FatherId == B.Id) { result += 0.125; } //check if Mother is also Grandmother
                else if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Id) { result += 0.0625; } //check if mother is fathers Great Grandmother
                else if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Id) { result += 0.0625; }
                else if (A.Litter.Mother.Litter.Father.Litter.Father.Litter.FatherId == B.Id) { result += 0.03125; } //check if Mother is fathers Great Great Grandmother
                else if (A.Litter.Mother.Litter.Mother.Litter.Father.Litter.FatherId == B.Id) { result += 0.03125; }
                else if (A.Litter.Mother.Litter.Father.Litter.Mother.Litter.FatherId == B.Id) { result += 0.03125; }
                else if (A.Litter.Mother.Litter.Mother.Litter.Mother.Litter.FatherId == B.Id) { result += 0.03125; }
                //To do: get mothers inbreeding (F) and calculate: result = result * 1.F
            }


            else
            {       //*****(n=3)******
                    if (A.Litter.FatherId == B.Litter.FatherId) { result += 0.125; } // A and B have same father 
                    if (A.Litter.MotherId == B.Litter.MotherId) { result += 0.125; } // A and B have same mother

                    // Check for no 2.  Grandfather/granddaughter or grandmother/grandson → 12.5% (1⁄8)
                    if (A.Id == B.Litter.Father.Litter.FatherId) { result += 0.125; } // A is B´s Grandfather (farfar)
                    if (A.Id == B.Litter.Mother.Litter.FatherId) { result += 0.125; } // A is B´s Grandfather (morfar)
                    if (A.Litter.Mother.Litter.MotherId == B.Id) { result += 0.125; } // B is A´s Grandmother (mormor)
                    if (A.Litter.Father.Litter.MotherId == B.Id) { result += 0.125; } // B is A´s Grandmother (farmor)



                    // Check for no 4.  Uncle/niece or aunt/nephew → 12.5% (1⁄8), if full related, half is 6.25%
                    // ***(n=4)***
                    if (A.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.0625; } //A´s Father is B´s Grandpa (farfar)
                    if (A.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.0625; } //A´s Mother is B´s Grandmother (farmor)
                    if (A.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.0625; } //A´s Father is B´s Grandpa (morfar)
                    if (A.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.0625; } //A´s Mother is B´s Grandmother (farmor)
                    if (A.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.0625; } //B´s Father is A´s Grandpa (farfar)
                    if (A.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.0625; } //B´s Mother is A´s Grandmother (farmor)
                    if (A.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.0625; } //B´s Father is A´s Grandpa (morfar)
                    if (A.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.0625; } //B´s Mother is A´s Grandmother (farmor)

                    // 5.  Great-grandfather/great-granddaughter or great-grandmother/great-grandson → 6.25% (1⁄16)
                    if (A.Id == B.Litter.Father.Litter.Father.Litter.FatherId) { result += 0.0625; } //A is B´s GreatGrandpa (either mom´s
                    if (A.Id == B.Litter.Mother.Litter.Father.Litter.FatherId) { result += 0.0625; } //                or dad/granddad´s side)
                    if (A.Id == B.Litter.Father.Litter.Mother.Litter.FatherId) { result += 0.0625; }
                    if (A.Id == B.Litter.Mother.Litter.Mother.Litter.FatherId) { result += 0.0625; }

                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Id) { result += 0.0625; } //B is A´s GreatGrandmother (either mom´s
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Id) { result += 0.0625; } //                or dad´s side)
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Id) { result += 0.0625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Id) { result += 0.0625; }

                    // 6.  Half-uncle/niece or half-aunt/nephew → 6.25% (1⁄16) --> covered under nr.4

                    // 7.  First cousins → 6.25% (1⁄16) - Syskina börn - sami afi og amma, if two lines under 8. are true
                    // 8.  Half First cousins → 3.125% (1⁄32) - Either grandma og grandpa the same
                    // ***(n=5)***                             
                    if (A.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.03125; }  //  Same grandfather                
                    if (A.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.03125; }
                    if (A.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.03125; }
                    if (A.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.03125; }

                    if (A.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.03125; }  //  Same grandmother
                    if (A.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.03125; }
                    if (A.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.03125; }
                    if (A.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.03125; }


                    // 9.  → 6,25% (1⁄32) - Great Grandma is the mother (Great-uncle/ neice and Great-aunt/ uncle)
                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.03125; } //A´s great grandmother is B´s mother
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.03125; }
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.03125; }
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.03125; }

                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.03125; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.03125; }
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.03125; }
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.03125; }

                    if (B.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.03125; }
                    if (B.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.03125; }
                    if (B.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.03125; }
                    if (B.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.03125; }

                    if (B.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.03125; }
                    if (B.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.03125; }
                    if (B.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.03125; }
                    if (B.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.03125; }

                    //10.  ********n=6******** adding 0.015625
                    //1 generation against 4 Generations: 
                    //short generation from mother
                    if (A.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }//A´s Great great grandmother is B´s mother
                    if (A.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }

                    if (A.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.MotherId) { result += 0.015625; }

                    if (A.Litter.Father.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }//A´s Great great grandpa is B´s father
                    if (A.Litter.Father.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }

                    if (A.Litter.Mother.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.FatherId) { result += 0.015625; }

                    //1 generation against 4 Generations: 
                    // short generation from father
                    if (B.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }//A´s Great great grandmother is B´s mother
                    if (B.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }

                    if (B.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.MotherId) { result += 0.015625; }

                    if (B.Litter.Father.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }//A´s Great great grandpa is B´s father
                    if (B.Litter.Father.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }

                    if (B.Litter.Mother.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.FatherId) { result += 0.015625; }

                    //2 Generations on one side against 3 Generations on other side
                    //shorter up from Mother
                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.015625; }//A´s Great grandma is B´s grandma
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.MotherId) { result += 0.015625; }

                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.MotherId) { result += 0.015625; }

                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.015625; }//A´s Great grandfather is B´s grandma
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.FatherId) { result += 0.015625; }

                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.FatherId) { result += 0.015625; }

                    //2 Generations on one side against 3 Generations on other side
                    //shorter up from Father
                    if (B.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.Father.Litter.MotherId) { result += 0.015625; }//B´s Great grandma is A´s grandma
                    if (B.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.Father.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.Father.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.Father.Litter.MotherId) { result += 0.015625; }

                    if (B.Litter.Father.Litter.Father.Litter.MotherId == A.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.MotherId == A.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.MotherId == A.Litter.Mother.Litter.MotherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.MotherId == A.Litter.Mother.Litter.MotherId) { result += 0.015625; }

                    if (B.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.Father.Litter.FatherId) { result += 0.015625; }//B´s Great grandfather is A´s grandma
                    if (B.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.Father.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.Father.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.Father.Litter.FatherId) { result += 0.015625; }

                    if (B.Litter.Father.Litter.Father.Litter.FatherId == A.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.FatherId == A.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.FatherId == A.Litter.Mother.Litter.FatherId) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.FatherId == A.Litter.Mother.Litter.FatherId) { result += 0.015625; }

                    //O generation up from one side, 5 generations up from other side
                    //mother
                    if (A.Litter.Father.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; } //A´s great great great grandmother is B
                    if (A.Litter.Father.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }

                    if (A.Litter.Father.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Father.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }

                    if (A.Litter.Mother.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }

                    if (A.Litter.Mother.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == B.Id) { result += 0.015625; }
                    if (A.Litter.Mother.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == B.Id) { result += 0.015625; }

                    //father
                    if (B.Litter.Father.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; } //B´s great great great grandmother is A
                    if (B.Litter.Father.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }

                    if (B.Litter.Father.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Father.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }

                    if (B.Litter.Mother.Litter.Father.Litter.Father.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.Father.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.Mother.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Father.Litter.Mother.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }

                    if (B.Litter.Mother.Litter.Mother.Litter.Father.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Father.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Mother.Litter.Father.Litter.MotherId == A.Id) { result += 0.015625; }
                    if (B.Litter.Mother.Litter.Mother.Litter.Mother.Litter.Mother.Litter.MotherId == A.Id) { result += 0.015625; }


                    //****n=7******
                    //Great Greandparents 
                //if great grandpa the same
                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.Father.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.Father.Litter.FatherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.Father.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.Father.Litter.FatherId ){ result += 0.0078125; }
                    
                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.Mother.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.Mother.Litter.FatherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.Mother.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.Mother.Litter.FatherId ){ result += 0.0078125; }

                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.Father.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.Father.Litter.FatherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Mother.Litter.Father.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Mother.Litter.Father.Litter.FatherId ){ result += 0.0078125; }
                    
                    if (A.Litter.Father.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.Mother.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.Mother.Litter.FatherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.FatherId == B.Litter.Father.Litter.Mother.Litter.FatherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.FatherId == B.Litter.Father.Litter.Mother.Litter.FatherId ){ result += 0.0078125; }

                //if great grandma the same
                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.Father.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.Father.Litter.MotherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.Father.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.Father.Litter.MotherId ){ result += 0.0078125; }
                    
                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.Mother.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.Mother.Litter.MotherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.Mother.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.Mother.Litter.MotherId ){ result += 0.0078125; }

                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.Father.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.Father.Litter.MotherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Mother.Litter.Father.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Mother.Litter.Father.Litter.MotherId ){ result += 0.0078125; }
                    
                    if (A.Litter.Father.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.Mother.Litter.MotherId ){ result += 0.0078125; } 
                    if (A.Litter.Father.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.Mother.Litter.MotherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Father.Litter.MotherId == B.Litter.Father.Litter.Mother.Litter.MotherId ){ result += 0.0078125; }
                    if (A.Litter.Mother.Litter.Mother.Litter.MotherId == B.Litter.Father.Litter.Mother.Litter.MotherId) { result += 0.0078125; }


                }
             result = result*100;
             return result;
        }




    }
}
