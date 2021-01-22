﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService;
using Services.UniverseService.Extensions.IOC;

namespace App.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services.UniverseAddEfService()
                .AddScoped(typeof(MainApp))
                .BuildServiceProvider();

        /*public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { } */
    }
}