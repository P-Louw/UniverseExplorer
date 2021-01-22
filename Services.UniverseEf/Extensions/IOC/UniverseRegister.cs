using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService.Context;

namespace Services.UniverseService.Extensions.IOC
{
    public static class UniverseRegister
    {
        public static IServiceCollection UniverseAddEfService(this IServiceCollection services)
        {
            services.AddDbContext<UniverseContext>()
                .AddScoped<IUniverseService, UniverseService>();
            
            return services;
        }
    }
}