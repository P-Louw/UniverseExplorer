using Microsoft.EntityFrameworkCore;
using Services.UniverseService.Context;

namespace Tests.Services.UniverseEf
{
    public class DbContextStub : UniverseContext
    {
        public ModelBuilder ModelBuilderStub { get; set; }

        public DbContextStub(DbContextOptions<UniverseContext> options) : base(options)
        { }
    }
}