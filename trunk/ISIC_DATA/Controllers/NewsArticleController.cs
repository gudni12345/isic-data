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
    public class NewsArticleController : Controller
    {

        private DogContext db = new DogContext();
       // private int displayNewsArticles = 10;
        //
        // GET: /NewsArticle/        
        public ActionResult Index()
        {
            return View(db.NewsArticle.ToList());
        }

        //
        // GET: /NewsArticle/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /NewsArticle/Create

        [HttpGet]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult CreateNews()
        {
            var model = new NewsArticle();

            ViewBag.HtmlContent = model.Content;
            return View(model);
        }

        //
        // POST: /NewsArticle/Create

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult CreateNews(NewsArticle model) 
        {
            ViewBag.HtmlContent = model.Content;
            model.Date = DateTime.Now;

            model.UsersId = WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);

            if (ModelState.IsValid)
            {                
                
                db.NewsArticle.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "NewsArticle");  // Success
                //return RedirectToAction("Index");  þessi er ekki til ennþá.

            }
         
            return View(model);
        }
         
        
        //
        // GET: /NewsArticle/Edit/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult EditNews(int id=0)
        {
            NewsArticle news = db.NewsArticle.Find(id);
            if (news == null)
            {
                return View("Error");
            }
            return View(news);
        }

        //
        // POST: /NewsArticle/Edit/5

        
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult EditNews(NewsArticle news)
        {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


           
            
                return View(news);
         }

              
        //
        // GET: /NewsArticle/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Delete(int id = 0)
        {

            NewsArticle news = db.NewsArticle.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
            
        }

        //
        // POST: /NewsArticle/Delete/5
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsArticle news = db.NewsArticle.Find(id);
            db.NewsArticle.Remove(news);
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
