using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using App.CLIghtFramework.Extensions.Hosting;
using App.UniverseExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace App.Core
{
    class Program
    {
        public static async Task Main(string[] args) =>
            CreateHostBuilder(args).Build()
                .Services.GetService<MainWindow>()
                .OnWindowLoad();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .CLIghtUseStartup<Startup>();
    }
}