using HexaEmployee.EfInfraData.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;

namespace HexaEmployee.EfInfraData.Tests.Fixtures
{
    public class DbContextFixture : HexaEmployeeDbContext
    {
        public DbContextFixture(DbContextOptions<HexaEmployeeDbContext> options)
            : base(options)
        {
        }

        public static DbContextFixture BuildContext()
        {
            var options = new DbContextOptions<HexaEmployeeDbContext>();
            var context = new DbContextFixture(options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Seed<T>(IEnumerable<T> seed)
            where T : class
        {
            Set<T>().AddRange(seed);
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase(databaseName: "TestMemoryDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
