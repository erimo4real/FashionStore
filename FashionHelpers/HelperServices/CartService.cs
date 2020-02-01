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
     public class CartService
    {
        private readonly IGenericRepository<Tbl_Cart> respository;
        public FashionAppDBEntities context = new FashionAppDBEntities();

        public CartService()
        {
            this.respository = new GenericRepository<Tbl_Cart>();
        }

        public bool Delete(int id)
        {
            respository.Delete(id);
            respository.Save();
            return true;
        }
    }
}
