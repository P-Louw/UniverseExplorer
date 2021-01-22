using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Services.UniverseService
{
    public static class UniverseRegister
    {
        public static ServiceCollection EfService(this ServiceCollection services)
        {
            services.AddDbContext<UniverseContext>()
                .AddScoped<IUniverseService, UniverseService>();
            
            return services;
        }
    }
}