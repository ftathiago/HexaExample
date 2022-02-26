using HexaEmployee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaEmployee.EfInfraData.OrmMappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder
                .ToTable("EMPLOYEE")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnName("EMAIL")
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(x => x.LastSalaryRaise)
                .HasColumnName("SALARY_RAISE_DATE")
                .IsRequired();

            builder
                .Property(x => x.Salary)
                .HasColumnName("SALARY")
                .HasColumnType("MONEY")
                .IsRequired();
        }
    }
}
