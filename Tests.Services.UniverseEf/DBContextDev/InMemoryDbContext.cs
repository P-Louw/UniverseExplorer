using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService.Context;

namespace Tests.Services.UniverseEf.DBContextDev
{
    public class ContextBuilder
    {
        public static DbContextOptions<UniverseContext> GetInMemoryDbContextOptions(string dbName = "TestUniverseIM") =>
            new DbContextOptionsBuilder<UniverseContext>()
                .UseInMemoryDatabase(databaseName: dbName, 
                    new InMemoryDatabaseRoot()).Options;

        public static void Init(IServiceProvider services)
        {
            using (var context = new UniverseContext(services.GetRequiredService
                <DbContextOptions<UniverseContext>>()))
                {
                    // TODO: If on model does not get called override data call.
                }
        }
    }
    
    public class InMemoryUniverseTest
    {
        protected DbContextOptions<UniverseContext> ContextOptions { get; }

        protected InMemoryUniverseTest(DbContextOptions<UniverseContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }
    }

   
}