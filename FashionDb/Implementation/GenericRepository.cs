using FashionDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionDb.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private FashionAppDBEntities _context = null;
        private DbSet<T> entity = null;

        public GenericRepository()
        {
            this._context = new FashionAppDBEntities();
            entity = _context.Set<T>();
        }

        //public GenericRepository(AppDBContext _context)
        //{
        //    this._context = _context;
        //    entity = _context.Set<T>();
        //}

        public IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        public T GetById(object id)
        {
            return entity.Find(id);
        }

        public void Insert(T obj)
        {
            entity.Add(obj);
        }

        public void Update(T obj)
        {
            entity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = entity.Find(id);
            entity.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
