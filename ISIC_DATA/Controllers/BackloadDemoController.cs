using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISIC_DATA.Controllers
{
    public class BackloadDemoController : Controller
    {
        //
        // GET: /BackupDemo/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult uploadTest()
        {
            var file = Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("Test");
        }
    }
}
