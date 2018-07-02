using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.IO;

namespace PassStorageService.CORE
{
    public class CandyStore
    {
        //this class contains Candy
  
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string PBKDF2Service(string _Input, int _iterations)
        {
            Rfc2898PasswordEncoder passEncoder = new Rfc2898PasswordEncoder();
            string ret = Convert.ToBase64String(passEncoder.EncodePassword(_Input, 1000).Hash);

            return ret;
        }
    }
}
