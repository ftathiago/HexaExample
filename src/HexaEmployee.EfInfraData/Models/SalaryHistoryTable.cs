using System;

namespace HexaEmployee.EfInfraData.Models
{
    public record SalaryHistoryTable
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid EmployeeId { get; init; }

        public decimal Salary { get; init; }

        public DateTimeOffset SalaryRaisedAt { get; init; }

        public virtual EmployeeTable Employee { get; init; }
    }
}
