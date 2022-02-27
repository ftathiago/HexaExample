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
            : new(
                currentSalary?.Salary ?? 0,
                currentSalary?.SalaryRaisedAt ?? DateTimeOffset.Now)
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
            };

        public static (EmployeeTable Employee, SalaryHistoryTable SalaryHistory) ToTable(this EmployeeEntity employeeEntity)
        {
            return (
                new EmployeeTable
                {
                    Id = employeeEntity.Id,
                    Name = employeeEntity.Name,
                    Email = employeeEntity.Email,
                },
                new SalaryHistoryTable
                {
                    EmployeeId = employeeEntity.Id,
                    Salary = employeeEntity.Salary,
                    SalaryRaisedAt = employeeEntity.LastSalaryRaise,
                });
        }
    }
}
