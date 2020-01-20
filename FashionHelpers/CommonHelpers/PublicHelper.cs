using FashionDb;
using FashionHelpers.HelperServices;
using Omu.Encrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace FashionHelpers.CommonHelpers
{
    public class PublicHelper
    {
      
        private readonly int _saltSize = 10;
        private readonly int _ranNumber = 5;
        private UserServices memberService = new UserServices();
        public string HashPassword(string password)
        {
            string hash = string.Empty;
            Hasher hasher = new Hasher();
            hasher.SaltSize = _saltSize;

            hash = hasher.Encrypt(password);
            return hash;
        }

        public string AutoGeneratePassword(out string hash)
        {
            var hasher = new Hasher();
            hasher.SaltSize = _saltSize;
            string newPassword = RandomString(_ranNumber).ToLower();
            hash = hasher.Encrypt(newPassword);
            return newPassword;
        }

        public bool IsStringSameAsHash(string userPassword, string savedHash)
        {
            bool isSame = false;
            // hash password
            var hasher = new Hasher();
            hasher.SaltSize = _saltSize;
            isSame = hasher.CompareStringToHash(userPassword, savedHash);

            return isSame;
        }

        /// <summary>
        /// Returns a random string
        /// </summary>
        /// <param name="size">size of random string to return.</param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            for (var i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static int RandomDigit(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static List<T> GetEnumList<T>()
        {
            // validate that T is in fact an enum
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException();
            }

            return Enum.GetValues(typeof(T)).Cast<T>().ToList<T>();
        }

        public static void SetUserCookie(string cookie)
        {
            FormsAuthentication.SetAuthCookie(cookie, true);
        }
        public  void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public  bool IsAuthenticated()
        {
            return GetActiveUserId > 0;
        }

        public  tbl_Users GetActiveUser()
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
                    var userDetailsArray = userDetails.Split(',');
                    foreach (string userDetail in userDetailsArray)
                    {
                        if (userDetail.IndexOf("userid") > -1)
                        {
                            var id = userDetail.Split('=')[1];
                            if (int.TryParse(id, out userId))
                                return userId;
                        }
                    }
                }
                return userId;
            }
        }

        public  string UserId
        {
            get
            {
                string userId = "";
                var user = GetActiveUser();
                if (user != null)
                    userId = user.ID.ToString();
                return userId;
            }
        }
        public  Guid GuidCustomerId
        {
            get
            {
                Guid customerId = Guid.Empty;
                string userDetails = GetUserIdentifier();
                if (!string.IsNullOrWhiteSpace(userDetails))
                {
                    var userDetailsArray = userDetails.Split(',');

                    foreach (string userDetail in userDetailsArray)
                    {
                        if (userDetail.IndexOf("customerid") > -1)
                        {
                            var id = userDetail.Split('=')[1];
                            customerId = Guid.Parse(id);
                        }
                    }
                }
                return customerId;
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


        public  string UserName
        {
            get
            {
                var User = GetActiveUser();
                if (User != null)
                    return User.UserName;
                return "";
            }
        }

    }
}
