using HexaEmployee.EfInfraData.Models;
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

        public virtual DbSet<SampleTable> SampleTables { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
