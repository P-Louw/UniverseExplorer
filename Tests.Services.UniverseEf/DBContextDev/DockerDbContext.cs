using Microsoft.EntityFrameworkCore;
using Services.UniverseService.Context;

namespace Tests.Services.UniverseEf.DBContextDev
{
    public class DockerDbContext
    {
        public static DbContextOptions<UniverseContext> CreateOptionsDocker()
        {
            var builder = new DbContextOptionsBuilder<UniverseContext>()
                .UseSqlServer(
                    "Server=localhost,1433;Database=TestUniverseDb;User=sa;Password=Pa55w0rd")
                .Options;
            
            return builder;
        }
    }
}