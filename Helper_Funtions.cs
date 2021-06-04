using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApplication
{
    public class Helper_Funtions
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static ISession _session;
        public Helper_Funtions(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public static string GetValueSession()
        {            
            string value = _session.GetString("Name");

            return value;
        }

        public static string TestStr()
        {
            return "Test Str";
        }

        public static string GetSession(string strjson, string key)
        {
            dynamic sessionInfo = JsonConvert.DeserializeObject(strjson);

            string value = sessionInfo[key];

            return value;

        }

        public static bool CheckPer(string strper, string roler, string key)
        {
            if (!string.IsNullOrEmpty(strper))
            {
                dynamic info = JsonConvert.DeserializeObject(strper);

                foreach (var item in info)
                {
                    if (item["Roler"] == roler && item[key] == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
