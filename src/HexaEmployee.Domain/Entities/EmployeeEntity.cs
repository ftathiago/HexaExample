using System;

namespace HexaEmployee.Domain.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Email { get; init; }

        public decimal Salary { get; init; }

        public DateTimeOffset LastSalaryRaise { get; init; }
    }
}
