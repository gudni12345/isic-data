﻿using System;
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
    public class ColorController : Controller
    {
        private DogContext db = new DogContext();

        //
        // GET: /Color/

        public ActionResult Index()
        {
            return View(db.Color.ToList());
        }

        //
        // GET: /Color/Details/5

        public ActionResult Details(int id = 0)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        //
        // GET: /Color/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Color/Create

        [HttpPost]
        public ActionResult Create(Color color)
        {
            if (ModelState.IsValid)
            {
                db.Color.Add(color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(color);
        }

        //
        // GET: /Color/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        //
        // POST: /Color/Edit/5

        [HttpPost]
        public ActionResult Edit(Color color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }

        //
        // GET: /Color/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        //
        // POST: /Color/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Color color = db.Color.Find(id);
            db.Color.Remove(color);
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