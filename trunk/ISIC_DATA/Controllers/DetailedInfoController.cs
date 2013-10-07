using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;

namespace ISIC_DATA.Controllers
{
    public class DetailedInfoController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /DetailedInfo/

        public ActionResult Index()
        {
            return View(db.DetailedInfo.ToList());
        }

        //
        // GET: /DetailedInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            DetailedInfo detailedinfo = db.DetailedInfo.Find(id);
            if (detailedinfo == null)
            {
                return HttpNotFound();
            }
            return View(detailedinfo);
        }

        //
        // GET: /DetailedInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DetailedInfo/Create

        [HttpPost]
        public ActionResult Create(DetailedInfo detailedinfo)
        {
            if (ModelState.IsValid)
            {
                db.DetailedInfo.Add(detailedinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detailedinfo);
        }

        //
        // GET: /DetailedInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DetailedInfo detailedinfo = db.DetailedInfo.Find(id);
            if (detailedinfo == null)
            {
                return HttpNotFound();
            }
            return View(detailedinfo);
        }

        //
        // POST: /DetailedInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(DetailedInfo detailedinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailedinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detailedinfo);
        }

        //
        // GET: /DetailedInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DetailedInfo detailedinfo = db.DetailedInfo.Find(id);
            if (detailedinfo == null)
            {
                return HttpNotFound();
            }
            return View(detailedinfo);
        }

        //
        // POST: /DetailedInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailedInfo detailedinfo = db.DetailedInfo.Find(id);
            db.DetailedInfo.Remove(detailedinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}