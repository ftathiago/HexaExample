using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HexaEmployee.EfInfraData.Contexts
{
    public class HexaEmployeeDbContext : DbContext
    {
        public HexaEmployeeDbContext(DbContextOptions<HexaEmployeeDbContext> options)
            : base(options)
        {
        }

        protected HexaEmployeeDbContext()
        {
            // Just for mocking test
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
