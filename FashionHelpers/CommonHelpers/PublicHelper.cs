using Omu.Encrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHelpers.CommonHelpers
{
    public class PublicHelper
    {
        private readonly int _saltSize = 10;
        private readonly int _ranNumber = 5;
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

    }
}
