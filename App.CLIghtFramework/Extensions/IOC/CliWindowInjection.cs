﻿using App.CLIghtFramework.Windows;
using App.CLIghtFramework.Windows.Context;
using Microsoft.Extensions.DependencyInjection;

namespace App.CLIghtFramework.Extensions.IOC
{
    public static class CliWindowInjection
    {
        public static IServiceCollection CLIghtAddWindowResolver(this IServiceCollection service)
        {
            // TODO: register windows that have been decorated or inherited from base window.
            
            return service;
        }

        public static IServiceCollection CLIghtAddDefaultWindow<T>(this IServiceCollection services) 
            where T : CLIghtWindow
        {
            services.AddScoped<ICLIghtContext, T>();
            return services;
        }
    }
}