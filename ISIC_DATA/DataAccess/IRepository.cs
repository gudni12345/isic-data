using ISIC_DATA.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace ISIC_DATA.DataAccess
{
    public interface IRepository<T>
    {
    //    IQueryable<Dog> FindAllDogs();
   //     void Add(Dog dog);
   //     void Delete(Dog dog);
    //    void Save();
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Dog Find(int id);
        DbEntityEntry Entry(object o);
        void InsertOrUpdate(T dog);
        void Dispose();
        int SaveChanges();
    }
}
