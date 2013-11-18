using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ISIC_DATA.Models;
using System.Data;

namespace ISIC_DATA.DataAccess
{
    public class DogRepository 
    {
 
        DogContext db = new DogContext();

        public DogRepository()
        {
            // TODO: Complete member initialization
        }


        public void InsertOrUpdateDog(Dog dog)
        {
            if (dog.Id == default(int))
            {
                // New entity
                db.Dog.Add(dog);
            }
            else
            {
                // Existing entity
                db.Entry(dog).State = EntityState.Modified;
            }
        }

        public void UpdateDog(Dog dog)
        {
            db.Entry(dog).State = EntityState.Modified;
        }



        public IQueryable<Dog> allDogs
        {
            get { return db.Dog; }
        }

        public IQueryable<Color> allColors
        {
            get { return db.Color; }
        }
        
        public IQueryable<Litter> allLitters
        {
            get { return db.Litter; }
        }

        public IQueryable<Country> allCountries
        {
            get { return db.Country; }
        }

        public IQueryable<Person> allPersons
        {
            get { return db.Person; }
        }

        public IQueryable<NewsArticle> allNewsArticles
        {
            get { return db.NewsArticle; }
        }


        public Dog FindDog(int id)
        {
            return db.Dog.SingleOrDefault(d => d.Id == id);
        }
        
        public Litter FindLitter(int id)
        {
            return db.Litter.SingleOrDefault(d => d.Id == id);
        }

        public Color FindColor(int id)
        {
            return db.Color.SingleOrDefault(d => d.Id == id);
        }

        public Country FindCountry(int id)
        {
            return db.Country.SingleOrDefault(d => d.Id == id);
        }

        public Person FindPerson(int id)
        {
            return db.Person.SingleOrDefault(d => d.Id == id);
        }

        public NewsArticle FindNewsArticle(int id)
        {
            return db.NewsArticle.SingleOrDefault(d => d.Id == id);
        }


        public void DeleteDog(int id)
        {
            var dog = FindDog(id);
            db.Dog.Remove(dog);
        }

        public void DeleteLitter(int id)
        {
            var litter = db.Litter.Find(id);
            db.Litter.Remove(litter);
        }

        public int countAllDogs()
        {
            return allDogs.Count();
        }


        public void Dispose()
        {
            db.Dispose();           
        }

        public void SaveChanges()
        {
            db.SaveChanges();            
        }
    }
}