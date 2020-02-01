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

        public static int UserId
        {
            get
            {
                int userId = 0;
                var user = GetActiveUser();
                if (user != null)
                    userId = user.ID;
                return userId;
            }
        }

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


        public static string UserName
        {
            get
            {
                var User = GetActiveUser();
                if (User != null)
                    return User.UserName;
                return "";
            }
        }

        public static IEnumerable<CartViewModel> Carts()
        {
            IEnumerable<CartViewModel> cartRecord;
            int currentUser = Convert.ToInt32(UserId);
            if (IsAuthenticated())
            {
                List<tbl_Users> user = _context.tbl_Users.ToList();
                List<Tbl_Cart> cart = _context.Tbl_Cart.ToList();
                List<tbl_Product> prod = _context.tbl_Product.ToList();
                List<tbl_Category> cat = _context.tbl_Category.ToList();
                List<tbl_Brands> brand = _context.tbl_Brands.ToList();

                 cartRecord = from ct in cart
                                 join pd in prod on ct.ProductId equals pd.ProductId into table1
                                 from pd in table1.ToList()
                                 join us in user on ct.UserId equals us.ID into table2
                                 from us in table2.ToList()
                                 where ct.UserId == currentUser
                              select new CartViewModel
                                 {
                                     c = ct,
                                     p = pd,
                                     u = us
                                 };
                return cartRecord;
            }
            else
            {
                return null;
            }
        }

        public static List<Tbl_Cart> CartList()
        {
            List<Tbl_Cart> currentCart = new List<Tbl_Cart>();

            int currentUser = Convert.ToInt32(UserId);

            if (IsAuthenticated())
            {
                currentCart = (from o in _context.Tbl_Cart
                               join d in _context.tbl_Product
                               on o.ProductId equals d.ProductId
                               join f in _context.tbl_Users
                               on o.UserId equals f.ID
                               where o.UserId == currentUser
                               select o).ToList();
                return currentCart;
            }
            else
            {
                return null;
            }

        }


        public class CartViewModel
        {
            public tbl_Users u { get; set; }
            public tbl_Product p { get; set; }
            public Tbl_Cart c { get; set; }
            public tbl_Category ca { get; set; }
            public tbl_Brands br { get; set; }
        }
    }
}
