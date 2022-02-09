using Demo.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Modules.Module2
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            
        }
    }
}
