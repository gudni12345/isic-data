using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;

namespace ISIC_DATA.Controllers
{
    public class RegisterDogController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new DogViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(DogViewModel viewModel)
        {

            return this.View(viewModel);
        }

    }
}
