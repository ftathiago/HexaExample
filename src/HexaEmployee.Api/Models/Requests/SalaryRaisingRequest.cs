using System;

namespace HexaEmployee.Api.Models.Requests
{
    public class SalaryRaisingRequest
    {
        public decimal NewSalary { get; init; }

        public DateTimeOffset NewSalaryRaisedAt { get; init; }
    }
}
