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
   public class ProductService
    {
        private readonly IGenericRepository<tbl_Product> respository;
        public FashionAppDBEntities context = new FashionAppDBEntities();

        public ProductService()
        {
            this.respository = new GenericRepository<tbl_Product>();
        }

        public IEnumerable<tbl_Product> GetAll()
        {
          var result =  respository.GetAll();
            return result;
        }
        public tbl_Product GetById(int id)
        {
            var result = respository.GetById(id);
            return result;
        }
        public void Insert(tbl_Product prod)
        {
            respository.Insert(prod);
            respository.Save();
        }

        public void Update(tbl_Product prod)
        {
            respository.Update(prod);
            respository.Save();
        }

        public bool Delete(int id)
        {
            respository.Delete(id);
            respository.Save();
            return true;
        }
    }
}
