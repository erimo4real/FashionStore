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
    public class UserServices
    {
        private readonly IGenericRepository<tbl_Users> respository;
        public FashionAppDBEntities context = new FashionAppDBEntities();

        public tbl_Users activeUser { get; set; }

        public UserServices()
        {
            this.respository = new GenericRepository<tbl_Users>();
        }

        public IEnumerable<tbl_Users> GetAllUser()
        {
            var result = respository.GetAll();
            return result;
        }

        public bool UsernameExists(string email)
        {
            bool result;
            var query = context.tbl_Users.Where(e => e.Email == email).SingleOrDefault();
            if (query == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public tbl_Users GetByEmail(string email)
        {
            var user = respository.GetAll();
            var member = user.Where(x => x.Email.ToLower() == email).SingleOrDefault();
            return member;
        }


        public tbl_Users GetById(int id)
        {
            var user = respository.GetById(id);
            return user;
        }
      
        public void Update(tbl_Users user)
        {
            respository.Update(user);
            respository.Save();
        }

        public void insert(tbl_Users user)
        {
            context.tbl_Users.Add(user);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            respository.Delete(id);
            respository.Save();
            return true;
        }
    }
}
