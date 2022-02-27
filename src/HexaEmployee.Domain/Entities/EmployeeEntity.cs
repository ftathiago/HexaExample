using HexaEmployee.Domain.Exceptions;
using System;

namespace HexaEmployee.Domain.Entities
{
    public class EmployeeEntity
    {
        public EmployeeEntity(decimal salary, DateTimeOffset lastSalaryRaise)
        {
            Salary = salary;
            LastSalaryRaise = lastSalaryRaise;
        }

        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Email { get; init; }

        public decimal Salary { get; private set; }

        public DateTimeOffset LastSalaryRaise { get; private set; }

        internal void RaiseSalaryFrom(
            (DateTimeOffset RaisedAt, decimal Salary) newSalary)
        {
            if (newSalary.RaisedAt < LastSalaryRaise)
            {
                throw new InvalidSalaryDateException(
                    currentRaiseDate: LastSalaryRaise,
                    newRaiseDate: newSalary.RaisedAt);
            }

            if (newSalary.Salary < Salary)
            {
                throw new InvalidSalaryValueException(
                    currentValue: Salary,
                    newValue: newSalary.Salary);
            }

            LastSalaryRaise = newSalary.RaisedAt;
            Salary = newSalary.Salary;
        }
    }
}
