using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Shorten_Urls.Models
{
    public static class Helpers
    {
        public readonly static int SizeOfRandomString = int.Parse(ConfigurationManager.AppSettings["LenghtOfRandomString"].ToString());
        public readonly static string WEBSITE_NAME = ConfigurationManager.AppSettings["WebsiteName"];
        public readonly static string WEBSITE_URL = ConfigurationManager.AppSettings["WebsiteUrl"];

        public static string GenerateRandomgUrl()
        {
            return RandomString(SizeOfRandomString);
        }
        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Random String</returns>
        /// <![CDATA[Taken from http://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c]]>
        private static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvxywz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <![CDATA[Taken from http://stackoverflow.com/questions/212510/what-is-the-easiest-way-to-encrypt-a-password-when-i-save-it-to-the-registry]]>
        public static string EncrypPassword(string password)
        {
            string saltedPassword = password /*+ Salt*/;
            byte[] data = Encoding.ASCII.GetBytes(saltedPassword);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public static string GetCurrentDomain()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            Uri uri = new Uri(url);
            string port = uri.Port != 80 ? ":" + uri.Port : "";
            string requested = uri.Scheme + Uri.SchemeDelimiter + uri.Host + port;
            return requested;
        }
    }
}
