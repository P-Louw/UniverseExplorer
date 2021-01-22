using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace App.Client
{
    class Program
    {
        public static async Task Main(string[] args) =>
            CreateHostBuilder(args).Build()
                .Services.GetService<MainWindow>()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseCliStartup<Startup>();
    }
}