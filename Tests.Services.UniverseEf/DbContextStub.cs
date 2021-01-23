using Microsoft.EntityFrameworkCore;

namespace Tests.Services.UniverseEf
{
    public class DbContextStub : DbContext
    {
        public ModelBuilder ModelBuilder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}