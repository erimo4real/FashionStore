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
   public  class ProductPublicHelper
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

        //public static int GetActiveProductId
        //{
        //    get
        //    {
        //        int userId = 0;
        //        string userDetails = GetUserIdentifier();
        //        if (!string.IsNullOrWhiteSpace(userDetails))
        //        {
        //            tbl_Users currentUser = memberService.GetByEmail(userDetails);
        //            Tbl_Cart currentProduct = _context.tbl_Product.Where(prod => prod. == currentUser.ID).SingleOrDefault();
        //            var id = currentCart.CartId.ToString();
        //            if (int.TryParse(id, out userId))
        //                return userId;
        //        }
        //        return userId;
        //    }
        //}

    }
}
