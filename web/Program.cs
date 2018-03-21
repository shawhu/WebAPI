using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Web;

namespace web
{
    public class Program
    {
        private static readonly NLog.Logger log = LogManager.GetLogger("Program");
        public static void Main(string[] args)
        {
            //log4net tryouts
            // XmlDocument log4netConfig = new XmlDocument();
            // if (File.Exists("log4net.config"))
            // {
            //     var repo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            //     XmlConfigurator.Configure(repo, new FileInfo("log4net.config"));
            //     log.Info("Main started.");
            // }

            //start web host
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .UseNLog()
                .Build();

            host.Run();
        }
    }
}
