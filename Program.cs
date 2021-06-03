using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApplication.Data;
using WebApplication.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
    }
    public class Global_Funtions
    {
        private static ApplicationDbContext Db { get; set; }
        public Global_Funtions(ApplicationDbContext context)
        {
            Db = context;
        }

        public static string CheckPermission(string username, string roler, string key)
        {
            //var model = Db.Permission.Where(p => p.Username == username && p.Roler == roler).First();
            ////var model = _db.Permission;
            //string value = model.Roler.ToString();
            //int count = model.Count();
            //if (model == null)
            //{
            //    return "OK";
            //}
            //else
            //{
            //    if (key == "index")
            //    {
            //        if (model.Index == true)
            //        {
            //            return true;
            //        }
            //    }
            //    if (key == "create")
            //    {
            //        if (model.Create == true)
            //        {
            //            return true;
            //        }
            //    }
            //    if (key == "edit")
            //    {
            //        if (model.Edit == true)
            //        {
            //            return true;
            //        }
            //    }
            //    if (key == "delete")
            //    {
            //        if (model.Delete == true)
            //        {
            //            return true;
            //        }
            //    }
            //    if (key == "approval")
            //    {
            //        if (model.Approval == true)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return model.Index.ToString();
            return "ok";
        }

        public static string GetSession(string strjson, string key)
        {
            dynamic sessionInfo = JsonConvert.DeserializeObject(strjson);

            string value = sessionInfo[key];

            return value;

        }

        public static string GetValue()
        {

            return "ABC";
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
