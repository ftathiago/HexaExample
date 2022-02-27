using HexaEmployee.Domain.Exceptions;
using HexaEmployee.Domain.Notifications;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.Shared.Extensions;
using System;
using System.Threading.Tasks;

namespace HexaEmployee.Domain.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private const string EmployeeNotFound = "The requested employee - {0} - does not exists.";
        private readonly IEmployee _employees;
        private readonly INotification _notifications;

        public EmployeeService(
            IEmployee employees,
            INotification notifications)
        {
            _employees = employees;
            _notifications = notifications;
        }

        public async Task UpdateSalaryAsync(
            (DateTimeOffset RaisedAt, decimal Salary) newSalary,
            Guid employeeId)
        {
            var registeredEmployee = await _employees.GetById(employeeId);
            if (registeredEmployee is null)
            {
                _notifications.AddMessage(ErrorCode.ExpectedDataNotFound, EmployeeNotFound.Format(employeeId));
                return;
            }

            registeredEmployee.RaiseSalaryFrom(newSalary);

            await _employees.UpdateAsync(registeredEmployee);
        }
    }
}
