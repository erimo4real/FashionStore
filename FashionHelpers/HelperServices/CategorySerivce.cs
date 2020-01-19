using FashionDb;
using FashionDb.Implementation;
using FashionDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHelpers.HelperServices
{
   public class CategorySerivce
    {
        private readonly IGenericRepository<tbl_Category> respository;
        public FashionAppDBEntities context = new FashionAppDBEntities();

        public CategorySerivce()
        {
            this.respository = new GenericRepository<tbl_Category>();
        }

        public IEnumerable<tbl_Category> GetAll()
        {
          var cat =  respository.GetAll().ToList();
            return cat;
        }

        public tbl_Category GetById(int id)
        {
          tbl_Category cat =  respository.GetById(id);
            return cat;
        }

        public void Update(tbl_Category cat)
        {
            context.Entry(cat).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Insert(tbl_Category category)
        {
            context.tbl_Category.Add(category);
            context.SaveChanges();
            
        }

        public void Delete(int id)
        {
          tbl_Category cat =  context.tbl_Category.Where(c => c.CategoryId == id).SingleOrDefault();
            context.tbl_Category.Remove(cat);
            context.SaveChanges();

        }
    }
}
