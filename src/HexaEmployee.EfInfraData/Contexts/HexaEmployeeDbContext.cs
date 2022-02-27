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

        public DbSet<EmployeeTable> Employees { get; init; }

        public DbSet<SalaryHistoryTable> SalaryHistory { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
