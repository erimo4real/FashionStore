using FashionDb;
using FashionHelpers.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FashionHelpers.CommonHelpers
{
    public class CartPublicHelper
    {
        private static UserServices memberService = new UserServices();
        private static FashionAppDBEntities _context = new FashionAppDBEntities();

        private static string GetUserIdentifier()
        {
            string userDetails = "";
            if (HttpContext.Current.User != null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    userDetails = HttpContext.Current.User.Identity.Name;
                }
            }
            return userDetails;
        }

        public static int GetActiveUserId
        {
            get
            {
                int userId = 0;
                string userDetails = GetUserIdentifier();
                if (!string.IsNullOrWhiteSpace(userDetails))
                {
                    tbl_Users currentUser = memberService.GetByEmail(userDetails);
                    var id = currentUser.ID.ToString();
                    if (int.TryParse(id, out userId))
                        return userId;

                }
                return userId;
            }
        }

        public static bool IsAuthenticated()
        {
            return GetActiveUserId > 0;
        }

        public static tbl_Users GetActiveUser()
        {
            if (IsAuthenticated())
            {
                tbl_Users User = memberService.GetById(GetActiveUserId);
                UserServices userViewModel = new UserServices();
                userViewModel.activeUser = User;

                return userViewModel.activeUser;
            }
            return null;
        }

        //public static int GetActiveCartId
        //{
        //    get
        //    {
        //        int userId = 0;
        //        string userDetails = GetUserIdentifier();
        //        if (!string.IsNullOrWhiteSpace(userDetails))
        //        {
        //            tbl_Users currentUser = memberService.GetByEmail(userDetails);
        //            Tbl_Cart currentCart = _context.Tbl_Cart.Where(cart => cart.UserId == currentUser.ID).SingleOrDefault();
        //            var id = currentCart.CartId.ToString();
        //            if (int.TryParse(id, out userId))
        //                return userId;
        //        }
        //        return userId;
        //    }
        //}


        public static IEnumerable<CartViewModel> GetActiveCartlist()
        {
            if (IsAuthenticated())
            {
                List<tbl_Product> products = _context.tbl_Product.ToList();
                List<Tbl_Cart> cart = _context.Tbl_Cart.ToList();
                List<tbl_Category> categories = _context.tbl_Category.ToList();
                List<tbl_Brands> brands = _context.tbl_Brands.ToList();
                List<tbl_Users> users = _context.tbl_Users.ToList();

                var productRecord = from c in cart
                                    join d in products on c.ProductId equals d.ProductId into table1
                                    from d in table1.ToList()
                                    join u in users on c.UserId equals u.ID into table2
                                    from u in table2.ToList()
                                    join e in categories on d.CategoryID equals e.CategoryId into table3
                                    from e in table3.ToList()
                                    join b in brands on d.BrandID equals b.ID into table4
                                    from b in table4.ToList()
                                    where c.UserId == GetActiveUserId
                                    select new CartViewModel
                                    {
                                         carts = c,
                                         pro = d,
                                         user = u,
                                         category = e,
                                         brands = b
                                    };
                return productRecord;
            }
            else
            {
                return null;
            }  
        }

        public class CartViewModel
        {
            public Tbl_Cart carts { get; set; }
            public tbl_Product  pro { get; set; }
            public tbl_Users user { get; set; }
            public tbl_Category category { get; set; }
            public tbl_Brands brands { get; set; }

        }
    }
}
