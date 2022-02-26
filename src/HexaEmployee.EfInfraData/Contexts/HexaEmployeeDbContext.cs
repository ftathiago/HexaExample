using HexaEmployee.Domain.Entities;
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

        public DbSet<EmployeeEntity> Employees { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
