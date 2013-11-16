using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;
using ISIC_DATA.Models;
using ISIC_DATA.DataAccess;

namespace ISIC_DATA.Tests.Fakes
{
    public class FakeDogRepository : DogRepository
    {
        private List<Dog> context;

        public FakeDogRepository(List<Dog> dogs)
        {
            context = dogs;
        }

        public IQueryable<Dog> All
        {
            get { return context.AsQueryable(); }
        }

        public IQueryable<Dog> AllIncluding(params System.Linq.Expressions.Expression<Func<Dog, object>>[] includeProperties)
        {
            IQueryable<Dog> query = All;
            foreach (var includeProperty in includeProperties)
            {
                // query = query.Include(includeProperty);
            }
            return query;
        }

        public void Delete(int id)
        {
            var dog = Find(id);
            context.Remove(dog);
        }

        public IQueryable<Dog> FindDogById(int id)
        {
            return from dog in All
                   where dog.Id == id                   
                   select dog;
        }

        public Dog Find(int id)
        {
            return context.Find(x => x.Id == id);
        }

        public void InsertOrUpdate(Dog dog)
        {
            context.Add(dog);
        }

        public int NumberOfDogs()
        {
            return context.Count();
        }

        public void Save()
        {
        }
    }
}
