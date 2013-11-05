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
            // We only get Id of the Dog backfrom the viewModel.
            Dog Father = db.Dog.Find(viewModel.Father.Id);
            Dog Mother = db.Dog.Find(viewModel.Mother.Id);

            double result = 0.0;
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

            ViewBag.Result = result;
            viewModel.Father.Name = Father.Name;
            viewModel.Mother.Name = Mother.Name;
            ModelState.Clear();
            return View(viewModel);
        }

    }
}
