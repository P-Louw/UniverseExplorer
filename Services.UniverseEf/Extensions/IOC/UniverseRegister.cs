using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService.Context;

namespace Services.UniverseService.Extensions.IOC
{
    public static class UniverseRegister
    {
        public static IServiceCollection UniverseAddEfService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UniverseContext>(options => 
                    options.UseSqlServer(config.GetConnectionString("DevelopUniverseDb")))
                .AddScoped<IUniverseService, UniverseService>();
            
            return services;
        }
    }
}