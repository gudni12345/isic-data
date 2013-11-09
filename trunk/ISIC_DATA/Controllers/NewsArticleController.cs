using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;
using PagedList;
namespace ISIC_DATA.Controllers
{
   
    public class NewsArticleController : Controller
    {

        private DogContext db = new DogContext();
         //
        // GET: /NewsArticle/  
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            ViewBag.CurrentFilter = searchString;
           

            var newsarticles = db.NewsArticle.Include(p =>p.Users);

            if (!String.IsNullOrEmpty(searchString))
            {
                    newsarticles = newsarticles.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())
                                   || p.Users.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "date_dec":
                    newsarticles = newsarticles.OrderByDescending(p => p.Date);
                    break;

                case "Date":
                    newsarticles = newsarticles.OrderBy(p => p.Date);
                    break;
                case "name_dec":
                    newsarticles = newsarticles.OrderBy(p => p.Users.Name);
                    break;
                                                   
                default:
                    newsarticles = newsarticles.OrderByDescending(p => p.Date);
                    break; 
                
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(newsarticles.ToPagedList(pageNumber, pageSize));
            //return View(db.NewsArticle.ToList());
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
            var categories = from CategoriesEnum s in Enum.GetValues(typeof(CategoriesEnum))
                             select new { Name = s.ToString().Replace('_', ' ') };
            ViewBag.CategoriesName = new SelectList(categories, "Name", "Name");
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
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "NewsArticle");  // Success

            }
         
            return View(model);
        }
         
        
        //
        // GET: /NewsArticle/Edit/5
        [HttpGet]
        [Authorize(Roles = "Administrator,SuperAdministrator")] 
        public ActionResult EditNews(int id=0)
        {
            NewsArticle news = db.NewsArticle.Find(id);
            var categories = from CategoriesEnum s in Enum.GetValues(typeof(CategoriesEnum))
                           select new { Name = s.ToString().Replace('_', ' ') };
            string selected = (from sub in db.NewsArticle
                               where sub.Id == id
                               select sub.CategoriesName).First();
            ViewBag.CategoriesName = new SelectList(categories, "Name", "Name", selected);



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
                news.UsersId = WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);
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
