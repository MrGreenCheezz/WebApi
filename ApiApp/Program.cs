using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/" + "Images"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + "Images");
            }
           
          CreateHostBuilder(args).Build().Run();         
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.UseStartup<Startup>();
                });

         
    }
}
