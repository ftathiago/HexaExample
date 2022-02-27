using HexaEmployee.EfInfraData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaEmployee.EfInfraData.OrmMappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<EmployeeTable>
    {
        public void Configure(EntityTypeBuilder<EmployeeTable> builder)
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
        }
    }
}
