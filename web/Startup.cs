using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Validation;

namespace web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //using console logger
            loggerFactory.AddConsole();
            //using log4net logger
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            //Register your ServiceStack AppHost as a .NET Core module
            app.UseServiceStack(new AppHost());
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
    public class AppHost : AppHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("WebAPI Service", typeof(MainService).GetAssembly())
        {
            var liveSettings = MapProjectPath("~/appsettings.txt");
            AppSettings = File.Exists(liveSettings)
                ? (IAppSettings)new TextFileSettings(liveSettings)
                : new AppSettings();
        }
        
        
        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                EnableFeatures = Feature.All
                .Remove(Feature.Html)
                //.Remove(Feature.Metadata)
                // .Remove(Feature.PredefinedRoutes)
                // .Add(Feature.ProtoBuf)
                ,
                DefaultContentType = MimeTypes.Json,
                DefaultRedirectPath = "/metadata", //set default|index|home
            });
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new CorsFeature());
            container.RegisterValidators(typeof(TestRequestValidator).GetAssembly());
            
            //Register Redis Client Manager singleton in ServiceStack's built-in Func IOC
            //container.Register<IRedisClientsManager>(new BasicRedisClientManager("localhost"));
        }
    }
}
