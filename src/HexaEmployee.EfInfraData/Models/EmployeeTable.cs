using System;

namespace HexaEmployee.EfInfraData.Models
{
    public record EmployeeTable
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Email { get; init; }
    }
}
