using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Assignment3.Services;
using Assignment3.Services.DataAccess;

namespace Assignment3.API
{
    public class Startup
    {
        private string _rootFolder;
        public Startup(IHostingEnvironment env)
        {
            _rootFolder = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public virtual void SetUpDatabase(IServiceCollection services) {
             // Add framework services.
            services.AddDbContext<AppDataContext>(options =>
                options.UseSqlite($"Data Source={_rootFolder}/RuTube.db"));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetUpDatabase(services);
            // Add framework services.
            services.AddMvc();
            services.AddTransient<IAccountDataMapper, AccountDataMapper>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ITokenDataMapper, TokenDataMapper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserDataMapper, UserDataMapper>();
            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<IVideoDataMapper, VideoDataMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
