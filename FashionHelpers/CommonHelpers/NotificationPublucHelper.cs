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
   public class NotificationPublucHelper
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

        public static TimeSpan getDatetime(string AddedTime)
        {
            TimeSpan timeSpent = new TimeSpan();
            if (IsAuthenticated())
            {
                AddedTime = "";
                string CurrentTime = DateTime.Now.ToString();

                 timeSpent = DateTime.Parse(CurrentTime).Subtract(DateTime.Parse(AddedTime));

                return timeSpent;
            }
            else
            {
                return timeSpent;
            }
        }
        public static List<tbl_Users> newuser()
        {
            List<tbl_Users> userprofile;
            if (IsAuthenticated())
            {
                userprofile = _context.tbl_Users.ToList();
                return userprofile;
            }
            else
            {
                return userprofile = null;
            }
           
        }
        public static IEnumerable<NotificationViewModel> Notification()
        {
            IEnumerable<NotificationViewModel> notificationRecord;

            int currentUser = Convert.ToInt32(UserId);

            if (IsAuthenticated())
            {
                List<tbl_Users> user = _context.tbl_Users.ToList();
                List<Notification> note = _context.Notifications.ToList();
                List<tbl_Product> prod = _context.tbl_Product.ToList();
                List<tbl_Category> cat = _context.tbl_Category.ToList();
                List<tbl_Brands> brand = _context.tbl_Brands.ToList();
                List<Tbl_Cart> cart = _context.Tbl_Cart.ToList();

                notificationRecord = from nt in note
                             join pd in prod on nt.productID equals pd.ProductId into table1
                             from pd in table1.ToList()
                             join us in user on nt.userID equals us.ID into table2
                             from us in table2.ToList()
                             join ct in cat on nt.categoryID equals ct.CategoryId into table3
                             from ct in table3.ToList()
                             join br in brand on nt.BrandID equals br.ID into table4
                             from br in table4.ToList()    
                             select new NotificationViewModel
                             {
                                 notification = nt,
                                 p = pd ,
                                 u = us,
                                 ca = ct,
                                 br = br
                            };
                return notificationRecord;
            }
            else
            {
                return null;
            }
        }

        public class NotificationViewModel
        {
            public Notification notification { get; set; }
            public tbl_Users u { get; set; }
            public tbl_Product p { get; set; }
            public Tbl_Cart c { get; set; }
            public tbl_Category ca { get; set; }
            public tbl_Brands br { get; set; }
            public Tbl_Cart ct { get;  set; }
        }

    }
}
