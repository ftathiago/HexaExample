using System;

namespace HexaEmployee.EfInfraData.Models
{
    public class EmployeeTable
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Email { get; init; }
    }
}
