using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService.Context;

namespace Tests.Services.UniverseEf.DBContextDev
{
    public class ContextBuilder
    {
        public static DbContextOptions<UniverseContext> InitDbInMem(string dbName = "TestUniverseIM") =>
            new DbContextOptionsBuilder<UniverseContext>()
                .UseInMemoryDatabase(databaseName: dbName, 
                    new InMemoryDatabaseRoot()).Options;

    }
    
    public static class UseDb
    {
        
        public static TCtx InMemory<TCtx>(
            Func<TCtx> context,
            Func<TCtx, TCtx> work) 
            where  TCtx : DbContext, IDisposable
        {
            using (var ctx = context())
                {
                    return work(ctx);
                    // Setup:

                    // Act:

                    // Assert:
                }
        }
    }

   
}