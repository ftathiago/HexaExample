using HexaEmployee.EfInfraData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaEmployee.EfInfraData.OrmMappings
{
    public class SalaryHistoryMapping : IEntityTypeConfiguration<SalaryHistoryTable>
    {
        public void Configure(EntityTypeBuilder<SalaryHistoryTable> builder)
        {
            builder
                .ToTable("EMPLOYEE_SALARY_HISTORY")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder
                .Property(x => x.EmployeeId)
                .HasColumnName("EMPLOYEE_ID")
                .IsRequired();

            builder
                .Property(x => x.Salary)
                .HasColumnName("SALARY")
                .HasColumnType("MONEY")
                .IsRequired();

            builder
                .Property(x => x.SalaryRaisedAt)
                .HasColumnName("RAISED_AT")
                .IsRequired();

            builder
                .HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
