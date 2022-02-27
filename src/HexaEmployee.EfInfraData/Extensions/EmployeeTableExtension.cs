#nullable enable
using HexaEmployee.Domain.Entities;
using HexaEmployee.EfInfraData.Models;
using System;

namespace HexaEmployee.EfInfraData.Extensions
{
    public static class EmployeeTableExtension
    {
        public static EmployeeEntity? ToEntity(
            this EmployeeTable? employee,
            SalaryHistoryTable? currentSalary) => employee is null
            ? default
            : new()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = currentSalary?.Salary ?? 0,
                LastSalaryRaise = currentSalary?.SalaryRaisedAt ?? DateTimeOffset.Now,
            };
    }
}
