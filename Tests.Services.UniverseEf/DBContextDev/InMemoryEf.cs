using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.UniverseService.Context;

namespace Tests.Services.UniverseEf.DBContextDev
{
    public class InMemoryEf : UniverseContext
    {
        public InMemoryEf()
            : base(InMemoryDbContextBuilder())
        { }

        protected static DbContextOptions<UniverseContext> InMemoryDbContextBuilder() =>
            new DbContextOptionsBuilder<UniverseContext>()
                .UseInMemoryDatabase(databaseName: "DEV:PlanetDb").Options;
    }

    public class ContextBuilder
    {
        public static void Init(IServiceProvider services)
        {
            using (var context = new UniverseContext(services.GetRequiredService
                <DbContextOptions<UniverseContext>>()))
                {
                    // TODO: If on model does not get called override data call.
                }
        }
    }
}