using Demo.Infrastructure;
using Demo.Infrastructure.Modules;
using Demo.WebHost.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebHost
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;
            services.AddModules(_hostingEnvironment.ContentRootPath);

            services.AddCustomizedMvc(GlobalConfiguration.Modules);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
            });

            var sp = services.BuildServiceProvider();

            var moduleInitializers = sp.GetServices<IModuleInitializer>();
            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.ConfigureServices(services);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    app.UseEndpoints(endpoints =>
            //    {
            //        endpoints.MapControllerRoute(
            //            name: "default",
            //            pattern: "{module=module-1}/{controller=Home}/{action=Index}/{id?}");
            //    });
            //});

            //// Adds endpoints defined in modules
            //var modules = app.ApplicationServices.GetRequiredService<IEnumerable<Module>>();
            //foreach (var module in modules)
            //{
            //    app.Map($"/{module.RoutePrefix}", builder =>
            //    {
            //        builder.UseRouting();
            //        module.Startup.Configure(builder, env);
            //    });
            //}
            app.UseCustomizedStaticFiles(env);
            app.UseCustomizedMvc();

            var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.Configure(app, env);
            }
        }
    }
}
