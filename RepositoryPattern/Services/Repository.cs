using RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using ContextDB;
using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AppDbContext db;
        private DbSet<T> table;

        public Repository( AppDbContext appDb)
        {
            db = appDb;
            table = db.Set<T>();

        }

        public void Delete(T obj)
        {
            table.Remove(obj);
            db.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            db.SaveChanges();
        }

        public void Update(T obj, int id)
        {
            var record = table.Find(id);
            record = obj;
            db.SaveChanges();
        }
    }
}
