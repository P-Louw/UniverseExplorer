using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService;

namespace App.Client
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => 
            Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services) =>
        /*services.AddDbContext<UniverseContext>()
            .AddScoped<IPlanetService, PlanetService>()*/
                .AddScoped(typeof(MainApp))
                .BuildServiceProvider();

        /*public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { } */
    }
}