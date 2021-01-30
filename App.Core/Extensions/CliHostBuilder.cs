using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Core.Extensions
{
    /// <summary>
    /// Extensions to enable <see cref="IHostBuilder"/> utility without running a webserver.
    /// </summary>
    public static class CliHostBuilder
    {
        private const string MethodNameConfigureServices = "ConfigureServices";

        /// <summary>
        /// Specify startup class to be used without webserver setup. when using this
        ///  a 'Run' or 'Start' method should be chained after the 'Build' method from <see cref="IHostBuilder"/>.
        ///  This should not be the native <see cref="IHostBuilder"/> run methods.
        /// </summary>
        /// <typeparam name="TStartup">The class that provides service registration, can contain optional ctor with param <see cref="IConfiguration"/>.</typeparam>
        /// <param name="builder">The <see cref="IHostBuilder"/> used to initialize with TStartup.</param>
        /// <param name="configMethodName">Optional specify name of method that registers services with <see cref="IServiceCollection"/> as a param. Default is 'ConfigureServices'.</param>
        /// <returns>Returns the same instance of <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CLIUseStartup<TStartup>(
            this IHostBuilder builder)
            where TStartup : class =>
            builder.ConfigureServices((context, services) =>
                {
                    // Find standard configure service method with signature ConfigureServices(IServiceCollection):
                    var configServiceMethod = typeof(TStartup).GetMethod(
                        MethodNameConfigureServices, new Type[] {typeof(IServiceCollection)});

                    // Check if ctor depends on IConfiguration:
                    bool dependsOnConfig = typeof(TStartup).GetConstructor(
                        new Type[] {typeof(IConfiguration)}) != null;

                    // Use config bool as switch to create the correct instance of startup class:
                    var instStartup = dependsOnConfig
                        ? (TStartup) Activator.CreateInstance(typeof(TStartup), context.Configuration)
                        : (TStartup) Activator.CreateInstance(typeof(TStartup), null);

                    configServiceMethod?.Invoke(instStartup, new object[] {services});
                });
        
        public static void CLIRun()
        {
            
        }

        public static Task CLIRunAsync()
        {
           return Task.CompletedTask; 
        }
    }
}