using System;
using System.Threading.Tasks;

namespace HexaEmployee.Domain.Services
{
    public interface IEmployeeService
    {
        public Task UpdateSalaryAsync(
            (DateTimeOffset RaisedAt, decimal Salary) newSalary,
            Guid employeeId);
    }
}
