using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using App.Core.Extensions;
using App.UniverseExplorer;
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
                .CLIUseStartup<Startup>();
    }
}